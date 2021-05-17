// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using SharpGen.Runtime;

namespace Vortice.Direct3D12
{
    public partial class ID3D12DeviceChild
    {
        public Result GetDevice<T>(out T? device) where T : ID3D12Device
        {
            Result result = GetDevice(typeof(T).GUID, out IntPtr nativePtr);
            if (result.Failure)
            {
                device = default;
                return result;
            }

            device = MarshallingHelpers.FromPointer<T>(nativePtr);
            return result;
        }

        public T? GetDevice<T>() where T : ID3D12Device
        {
            if (GetDevice(typeof(T).GUID, out IntPtr nativePtr).Failure)
            {
                return default;
            }

            return MarshallingHelpers.FromPointer<T>(nativePtr);
        }
    }
}
