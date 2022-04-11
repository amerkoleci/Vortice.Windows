// Copyright © Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

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
