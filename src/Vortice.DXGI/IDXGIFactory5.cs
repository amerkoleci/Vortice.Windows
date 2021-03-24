// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using SharpGen.Runtime;

namespace Vortice.DXGI
{
    public partial class IDXGIFactory5
    {
        /// <summary>
        /// Gets if tearing is allowed during present.
        /// </summary>
        public RawBool PresentAllowTearing
        {
            get
            {
                unsafe
                {
                    RawBool allowTearing;
                    CheckFeatureSupport(Feature.PresentAllowTearing, &allowTearing, sizeof(RawBool));
                    return allowTearing;
                }
            }
        }

        public bool CheckFeatureSupport<T>(Feature feature, T featureSupport) where T : unmanaged
        {
            unsafe
            {
                return CheckFeatureSupport(feature, &featureSupport, sizeof(T)).Success;
            }
        }
    }
}
