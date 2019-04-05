// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System.Runtime.InteropServices;

namespace SharpDirect2D
{
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public readonly struct WICRect
    {
        public readonly int X;
        public readonly int Y;
        public readonly int Width;
        public readonly int Height;

        public WICRect(int x, int y, int width, int height)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }

        public WICRect(int width, int height)
        {
            X = Y = 0;
            Width = width;
            Height = height;
        }
    }
}
