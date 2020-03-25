using System;
using System.Runtime.InteropServices;

namespace SharpGen.Runtime
{
    [Flags]
    public enum ComContext : uint
    {
        InprocServer = 0x1,
        InprocHandler = 0x2,
        LocalServer = 0x4,
        InprocServer16 = 0x8,
        RemoteServer = 0x10,
        InprocHandler16 = 0x20,
        Reserved1 = 0x40,
        Reserved2 = 0x80,
        Reserved3 = 0x100,
        Reserved4 = 0x200,
        NoCodeDownload = 0x400,
        Reserved5 = 0x800,
        NoCustomMarshal = 0x1000,
        EnableCodeDownload = 0x2000,
        NoFailureLog = 0x4000,
        DisableAaa = 0x8000,
        EnableAaa = 0x10000,
        FromDefaultContext = 0x20000,
        Inproc = InprocServer | InprocHandler,
        Server = InprocServer | LocalServer | RemoteServer,
        All = Server | InprocHandler
    }

    public static class ComUtilities
    {
        static ComUtilities()
        {
            if (!VorticePlatformDetection.IsUAP)
            {
                if (CoInitializeEx(IntPtr.Zero, COINIT_APARTMENTTHREADED) == RPC_E_CHANGED_MODE)
                {
                    CoInitializeEx(IntPtr.Zero, COINIT_MULTITHREADED);
                }
            }
        }

        
        public unsafe static void CreateComInstance(Guid classGuid, ComContext context, Guid interfaceGuid, ComObject comObject)
        {
            if (!VorticePlatformDetection.IsUAP)
            {
                var result = CoCreateInstance(classGuid, IntPtr.Zero, context, interfaceGuid, out IntPtr pointer);
                result.CheckError();
                comObject.NativePointer = pointer;
            }
            else
            {
                var localQuery = new MultiQueryInterface()
                {
                    InterfaceIID = new IntPtr(&interfaceGuid),
                    IUnknownPointer = IntPtr.Zero,
                    ResultCode = 0,
                };

                var result = CoCreateInstanceFromApp(classGuid, IntPtr.Zero, context, IntPtr.Zero, 1, ref localQuery);
                result.CheckError();
                localQuery.ResultCode.CheckError();
                comObject.NativePointer = localQuery.IUnknownPointer;
            }
        }

        public static unsafe bool TryCreateComInstance(Guid classGuid, ComContext context, Guid interfaceGuid, ComObject comObject)
        {
            if (!VorticePlatformDetection.IsUAP)
            {
                var result = CoCreateInstance(classGuid, IntPtr.Zero, context, interfaceGuid, out IntPtr pointer);
                comObject.NativePointer = pointer;
                return result.Success;
            }
            else
            {
                var localQuery = new MultiQueryInterface()
                {
                    InterfaceIID = new IntPtr(&interfaceGuid),
                    IUnknownPointer = IntPtr.Zero,
                    ResultCode = 0,
                };

                var result = CoCreateInstanceFromApp(classGuid, IntPtr.Zero, context, IntPtr.Zero, 1, ref localQuery);
                comObject.NativePointer = localQuery.IUnknownPointer;
                return result.Success && localQuery.ResultCode.Success;
            }
        }

        // WIN32
        private const uint RPC_E_CHANGED_MODE = 0x80010106;
        private const uint COINIT_MULTITHREADED = 0x0;
        private const uint COINIT_APARTMENTTHREADED = 0x2;

        [DllImport("ole32.dll", SetLastError = true, CallingConvention = CallingConvention.StdCall)]
        private static extern uint CoInitializeEx([In, Optional] IntPtr pvReserved, [In]uint dwCoInit);


        [DllImport("ole32.dll", ExactSpelling = true, EntryPoint = "CoCreateInstance", PreserveSig = true)]
        private static extern Result CoCreateInstance([In, MarshalAs(UnmanagedType.LPStruct)] Guid rclsid, IntPtr pUnkOuter, ComContext dwClsContext, [In, MarshalAs(UnmanagedType.LPStruct)] Guid riid, out IntPtr comObject);

        // UWP
        [StructLayout(LayoutKind.Sequential)]
        private struct MultiQueryInterface
        {
            public IntPtr InterfaceIID;
            public IntPtr IUnknownPointer;
            public Result ResultCode;
        }

        [DllImport("api-ms-win-core-com-l1-1-0.dll", ExactSpelling = true, EntryPoint = "CoCreateInstanceFromApp", PreserveSig = true)]
        private static extern Result CoCreateInstanceFromApp([In, MarshalAs(UnmanagedType.LPStruct)] Guid rclsid,
            IntPtr pUnkOuter,
            ComContext dwClsContext,
            IntPtr reserved,
            int countMultiQuery,
            ref MultiQueryInterface query);
    }
}
