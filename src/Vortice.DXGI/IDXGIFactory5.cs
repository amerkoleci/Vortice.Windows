// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using System.Runtime.CompilerServices;
using SharpGen.Runtime.Win32;

namespace Vortice.DXGI
{
    public partial class IDXGIFactory5
    {
        /// <summary>
        /// Gets if tearing is allowed during present.
        /// </summary>
        public unsafe bool PresentAllowTearing
        {
            get
            {
                RawBool allowTearing;
                CheckFeatureSupport(Feature.PresentAllowTearing, new IntPtr(&allowTearing), sizeof(RawBool));
                return allowTearing;
            }
        }

        public unsafe bool CheckFeatureSupport<T>(Feature feature, ref T featureSupport) where T : unmanaged
        {
            fixed (void* featureSupportPtr = &featureSupport)
            {
                return CheckFeatureSupport(feature, (IntPtr)featureSupportPtr, sizeof(T)).Success;
            }
        }
    }
}
