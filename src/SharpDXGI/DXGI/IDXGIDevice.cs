// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using SharpGen.Runtime;

namespace SharpDXGI
{
    public partial class IDXGIDevice
    {
        public Result QueryResourceResidency(IUnknown[] resources, Residency[] residencyStatus)
        {
            Guard.NotNullOrEmpty(resources, nameof(resources));
            Guard.NotNullOrEmpty(residencyStatus, nameof(residencyStatus));

            return QueryResourceResidency(resources, residencyStatus, resources.Length);
        }
    }
}
