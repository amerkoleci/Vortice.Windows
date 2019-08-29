// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace Vortice.Direct2D1
{
    public partial class ID2D1EffectContext
    {
        public Vector2 Dpi
        {
            get
            {
                GetDpi(out var dpiX, out var dpiY);
                return new Vector2(dpiX, dpiY);
            }
        }

        public unsafe T CheckFeatureSupport<T>(Feature feature) where T : struct
        {
            T featureSupport = default;
            CheckFeatureSupport(feature, new IntPtr(Unsafe.AsPointer(ref featureSupport)), Unsafe.SizeOf<T>());
            return featureSupport;
        }

        public unsafe bool CheckFeatureSupport<T>(Feature feature, ref T featureSupport) where T : struct
        {
            return CheckFeatureSupport(feature, new IntPtr(Unsafe.AsPointer(ref featureSupport)), Unsafe.SizeOf<T>()).Success;
        }
    }
}
