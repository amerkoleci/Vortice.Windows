using System;

namespace Vortice.DXGI
{
    public static class DebugInterface
    {
        public static readonly Guid All = new Guid("e48ae283-da80-490b-87e6-43e9a9cfda08");
        public static readonly Guid DX = new Guid("35cdd7fc-13b2-421d-a5d7-7e4451287d64");
        public static readonly Guid DXGI = new Guid("25cddaa4-b1c6-47e1-ac3e-98875b5a2e2a");
        public static readonly Guid App = new Guid("06cd6e01-4219-4ebd-8709-27ed23360c62");

        public static bool Get<T>(out IntPtr handle) where T : class
        {
            try
            {
                var guid = typeof(T).GUID;
                return Vortice.DXGIInternal.DXGIGetDebugInterface(guid, out handle).Success;
            }
            catch
            {
                handle = IntPtr.Zero;
                return false;
            }
        }
    }
}
