// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;

namespace Vortice
{
    public static class Utilities
    {
        public static void Dispose<T>(T[] values) where T : IDisposable
        {
            for(var i = 0; i < values.Length; i++)
            {
                if (values[i] != null)
                {
                    values[i].Dispose();
                }
            }
        }

        public static void Dispose<T>(List<T> values) where T : IDisposable
        {
            for (var i = 0; i < values.Count; i++)
            {
                if (values[i] != null)
                {
                    values[i].Dispose();
                }
            }
        }

        public static void Dispose<T>(IEnumerable<T> values) where T : IDisposable
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
