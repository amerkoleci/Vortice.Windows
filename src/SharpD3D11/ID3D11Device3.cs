// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

namespace SharpD3D11
{
    public partial class ID3D11Device3
    {
        public unsafe ID3D11DeviceContext3 CreateDeferredContext3()
        {
            return CreateDeferredContext3(0);
        }
    }
}
