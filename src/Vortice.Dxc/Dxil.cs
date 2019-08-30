using NativeLibraryLoader;

namespace Vortice.Dxc
{
    /// <summary>
    /// A <see langword="class"/> that can be used to load the dxil.dll library, if needed
    /// </summary>
    public static class Dxil
    {
        private static NativeLibrary s_dxilLib;

        /// <summary>
        /// Ensures that the dxil.dll library is loaded and ready to be used
        /// </summary>
        public static void LoadLibrary()
        {
            if (s_dxilLib == null)
                s_dxilLib = new NativeLibrary("dxil.dll");
        }
    }
}
