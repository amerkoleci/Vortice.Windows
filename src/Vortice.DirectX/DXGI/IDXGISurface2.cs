// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using SharpGen.Runtime;

namespace Vortice.DirectX.DXGI
{
    public partial class IDXGISurface2
    {
        public T GetResource<T>(out int subresourceIndex) where T : ComObject
        {
            if (GetResource(typeof(T).GUID, out var nativePtr, out subresourceIndex).Failure)
            {
                return default;
            }

            return FromPointer<T>(nativePtr);
        }
    }
}
