// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using SharpGen.Runtime;

namespace Vortice.DXGI
{
    public partial class IDXGISurface2
    {
        public T GetResource<T>(out int subresourceIndex) where T : ComObject
        {
            GetResource(typeof(T).GUID, out IntPtr nativePtr, out subresourceIndex).CheckError();
            return MarshallingHelpers.FromPointer<T>(nativePtr);
        }

        public Result GetResource<T>(out int subresourceIndex, out T? parentResource) where T : ComObject
        {
            Result result = GetResource(typeof(T).GUID, out IntPtr nativePtr, out subresourceIndex);
            if (result.Failure)
            {
                parentResource = default;
                return result;
            }

            parentResource = MarshallingHelpers.FromPointer<T>(nativePtr);
            return result;
        }
    }
}
