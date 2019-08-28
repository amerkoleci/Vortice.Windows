// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using SharpGen.Runtime;

namespace Vortice.DXGI
{
    public partial class IDXGIDevice
    {
        public Result QueryResourceResidency(IUnknown[] resources, Residency[] residencyStatus)
        {
            return QueryResourceResidency(resources, residencyStatus, resources.Length);
        }
    }
}
