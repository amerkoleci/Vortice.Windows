// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using System.Runtime.InteropServices;
using SharpGen.Runtime;

namespace SharpDXGI
{
    public static class ComUtilities
    {
        [Flags]
        public enum CLSCTX : uint
        {
            None = 0,
            ClsctxInprocServer = 0x1,
            ClsctxInprocHandler = 0x2,
            ClsctxLocalServer = 0x4,
            ClsctxInprocServer16 = 0x8,
            ClsctxRemoteServer = 0x10,
            ClsctxInprocHandler16 = 0x20,
            ClsctxReserved1 = 0x40,
            ClsctxReserved2 = 0x80,
            ClsctxReserved3 = 0x100,
            ClsctxReserved4 = 0x200,
            ClsctxNoCodeDownload = 0x400,
            ClsctxReserved5 = 0x800,
            ClsctxNoCustomMarshal = 0x1000,
            ClsctxEnableCodeDownload = 0x2000,
            ClsctxNoFailureLog = 0x4000,
            ClsctxDisableAaa = 0x8000,
            ClsctxEnableAaa = 0x10000,
            ClsctxFromDefaultContext = 0x20000,
            ClsctxInproc = ClsctxInprocServer | ClsctxInprocHandler,
            ClsctxServer = ClsctxInprocServer | ClsctxLocalServer | ClsctxRemoteServer,
            ClsctxAll = ClsctxServer | ClsctxInprocHandler
        }

#if WINDOWS_UWP
        [StructLayout(LayoutKind.Sequential)]
        public struct MultiQueryInterface
        {
            public IntPtr InterfaceIID;
            public IntPtr IUnknownPointer;
            public Result ResultCode;
        };


        [DllImport("api-ms-win-core-com-l1-1-0.dll", ExactSpelling = true, EntryPoint = "CoCreateInstanceFromApp", PreserveSig = true)]
        private static extern Result CoCreateInstanceFromApp([In, MarshalAs(UnmanagedType.LPStruct)] Guid rclsid, 
            IntPtr pUnkOuter, 
            CLSCTX dwClsContext, 
            IntPtr reserved,
            int countMultiQuery,
            ref MultiQueryInterface query);

        public unsafe static void CreateComInstance(Guid clsid, CLSCTX clsctx, Guid riid, ComObject comObject)
        {
            MultiQueryInterface localQuery = new MultiQueryInterface()
            {
                InterfaceIID = new IntPtr(&riid),
                IUnknownPointer = IntPtr.Zero,
                ResultCode = 0,
            };

            var result = CoCreateInstanceFromApp(clsid, IntPtr.Zero, clsctx, IntPtr.Zero, 1, ref localQuery);
            result.CheckError();
            localQuery.ResultCode.CheckError();
            comObject.NativePointer = localQuery.IUnknownPointer;
        }

        public unsafe static bool TryCreateComInstance(Guid clsid, CLSCTX clsctx, Guid riid, ComObject comObject)
        {
            MultiQueryInterface localQuery = new MultiQueryInterface()
            {
                InterfaceIID = new IntPtr(&riid),
                IUnknownPointer = IntPtr.Zero,
                ResultCode = 0,
            };

            var result = CoCreateInstanceFromApp(clsid, IntPtr.Zero, clsctx, IntPtr.Zero, 1, ref localQuery);
            comObject.NativePointer = localQuery.IUnknownPointer;
            return result.Success && localQuery.ResultCode.Success;
        }
#else
        private const uint RPC_E_CHANGED_MODE = 0x80010106;
        private const uint COINIT_MULTITHREADED = 0x0;
        private const uint COINIT_APARTMENTTHREADED = 0x2;

        static ComUtilities()
        {
            if (CoInitializeEx(IntPtr.Zero, COINIT_APARTMENTTHREADED) == RPC_E_CHANGED_MODE)
            {
                CoInitializeEx(IntPtr.Zero, COINIT_MULTITHREADED);
            }
        }

        public static void CreateComInstance(Guid clsid, CLSCTX clsctx, Guid riid, ComObject comObject)
        {
            var result = CoCreateInstance(clsid, IntPtr.Zero, clsctx, riid, out var nativePtr);
            result.CheckError();
            comObject.NativePointer = nativePtr;
        }

        public static bool TryCreateComInstance(Guid clsid, CLSCTX clsctx, Guid riid, ComObject comObject)
        {
            var result = CoCreateInstance(clsid, IntPtr.Zero, clsctx, riid, out var nativePtr);
            comObject.NativePointer = nativePtr;
            return result.Success;
        }

        [DllImport("ole32.dll", SetLastError = true, CallingConvention = CallingConvention.StdCall)]
        private static extern uint CoInitializeEx([In, Optional] IntPtr pvReserved, [In]uint dwCoInit);

        [DllImport("ole32.dll", ExactSpelling = true, EntryPoint = "CoCreateInstance", PreserveSig = true)]
        private static extern Result CoCreateInstance([In, MarshalAs(UnmanagedType.LPStruct)] Guid rclsid, IntPtr pUnkOuter, CLSCTX dwClsContext, [In, MarshalAs(UnmanagedType.LPStruct)] Guid riid, out IntPtr comObject);
#endif
    }
}
