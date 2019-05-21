// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System.Drawing;

namespace Vortice.DirectX.WIC
{
    public partial class IWICBitmapLock
    {
        public Size Size
        {
            get
            {
                GetSize(out var width, out var height);
                return new Size(width, height);
            }
        }

        public byte[] GetDataPointer()
        {
            var pointer = GetDataPointer(out var size);
            var data = new byte[size];
            Interop.Read(pointer, data);
            return data;
        }
    }
}
