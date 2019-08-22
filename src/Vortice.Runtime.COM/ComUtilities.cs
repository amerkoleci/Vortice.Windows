using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace SharpGen.Runtime
{
    public static class ComUtilities
    {
        [Flags]
        public enum CLSCTX : uint
        {
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

        internal unsafe static void CreateComInstance(Guid clsid, CLSCTX clsctx, Guid riid, ComObject comObject)
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

        internal unsafe static bool TryCreateComInstance(Guid clsid, CLSCTX clsctx, Guid riid, ComObject comObject)
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
        [DllImport("ole32.dll", ExactSpelling = true, EntryPoint = "CoCreateInstance", PreserveSig = true)]
        private static extern Result CoCreateInstance([In, MarshalAs(UnmanagedType.LPStruct)] Guid rclsid, IntPtr pUnkOuter, CLSCTX dwClsContext, [In, MarshalAs(UnmanagedType.LPStruct)] Guid riid, out IntPtr comObject);

        internal static void CreateComInstance(Guid clsid, CLSCTX clsctx, Guid riid, ComObject comObject)
        {
            IntPtr pointer;
            var result = CoCreateInstance(clsid, IntPtr.Zero, clsctx, riid, out pointer);
            result.CheckError();
            comObject.NativePointer = pointer;
        }

        internal static bool TryCreateComInstance(Guid clsid, CLSCTX clsctx, Guid riid, ComObject comObject)
        {
            IntPtr pointer;
            var result = CoCreateInstance(clsid, IntPtr.Zero, clsctx, riid, out pointer);
            comObject.NativePointer = pointer;
            return result.Success;
        }
#endif
    }
}
