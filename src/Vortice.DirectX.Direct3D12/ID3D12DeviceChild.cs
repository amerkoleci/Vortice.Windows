// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using System.Runtime.CompilerServices;

namespace Vortice.DirectX.Direct3D12
{
    public partial class ID3D12DeviceChild
    {
        private ID3D12Device _device;

        public ID3D12Device Device
        {
            get
            {
                if (_device == null)
                {
                    _device = GetDevice<ID3D12Device>();
                }

                return _device;
            }
        }

        public T GetDevice<T>() where T : ID3D12Device
        {
            if (GetDevice(typeof(T).GUID, out var nativePtr).Failure)
            {
                return default;
            }

            return FromPointer<T>(nativePtr);
        }
    }
}
