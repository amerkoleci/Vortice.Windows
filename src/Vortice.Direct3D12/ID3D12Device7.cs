// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

namespace Vortice.Direct3D12
{
    public partial class ID3D12Device7
    {
        public ID3D12StateObject AddToStateObject(StateObjectDescription addition, ID3D12StateObject stateObjectToGrowFrom)
        {
            return AddToStateObject(addition, stateObjectToGrowFrom, typeof(ID3D12StateObject).GUID);
        }

        public ID3D12ProtectedResourceSession1 CreateProtectedResourceSession1(ProtectedResourceSessionDescription1 description)
        {
            return CreateProtectedResourceSession1(ref description, typeof(ID3D12ProtectedResourceSession1).GUID);
        }
    }
}
