// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System.Runtime.InteropServices;

namespace Vortice.Gdi
{
    [StructLayout(LayoutKind.Sequential)]
    public struct FontSignature
    {
        public int fsUsb1;
        public int fsUsb2;
        public int fsUsb3;
        public int fsUsb4;
        public int fsCsb1;
        public int fsCsb2;
    }

}
