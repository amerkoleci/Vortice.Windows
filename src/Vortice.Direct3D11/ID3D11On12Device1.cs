// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using SharpGen.Runtime;

namespace Vortice.Direct3D11
{
    public partial class ID3D11On12Device1
    {
        public T GetD3D12Device<T>() where T : ComObject
        {
            var result = GetD3D12Device(typeof(T).GUID, out var devicePtr);
            if (result.Failure)
            {
                return default;
            }

            return FromPointer<T>(devicePtr);
        }
    }
}
