// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using System.Collections.Immutable;
using SharpGen.Runtime;

namespace Vortice.DXGI
{
    // TODO: Move to Base library?

    public static class Utilities
    {
        public static void ReleaseIfNotDefault<T>(ImmutableArray<T> values) where T : ComObject
        {
            if (!values.IsDefault)
            {
                foreach (var value in values)
                {
                    if (value != null)
                    {
                        value.Release();
                    }
                }
            }
        }

        public static void DisposeIfNotDefault<T>(ImmutableArray<T> values) where T : IDisposable
        {
            if (!values.IsDefault)
            {
                foreach (var value in values)
                {
                    if (value != null)
                    {
                        value.Dispose();
                    }
                }
            }
        }
    }
}
