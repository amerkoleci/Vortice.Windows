// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using SharpDXGI;
using Vortice.Mathematics;

namespace SharpDirect3D11
{
    public partial class ID3D11DeviceContext1
    {
        public void ClearView(ID3D11View view, Color4 color)
        {
            ClearView(view, color);
        }

        public void ClearView(ID3D11View view, Color4 color, InteropRect[] rects)
        {
            ClearView(view, color, rects, rects.Length);
        }

        public void DiscardView1(ID3D11View view, InteropRect[] rects)
        {
            DiscardView1(view, rects, rects.Length);
        }
    }
}
