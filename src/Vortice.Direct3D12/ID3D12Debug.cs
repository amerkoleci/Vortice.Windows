// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using SharpGen.Runtime;

namespace Vortice.Direct3D12
{
    public partial class ID3D12Debug
    {
        public static Result TryCreate(out ID3D12Debug debugInterface)
        {
            var result = D3D12.GetDebugInterface(typeof(ID3D12Debug).GUID, out var nativePtr);

            if (result.Failure)
            {
                debugInterface = null;
                return result;
            }

            debugInterface = new ID3D12Debug(nativePtr);
            return result;
        }
    }
}
