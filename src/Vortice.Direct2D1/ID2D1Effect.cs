// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace Vortice.Direct2D1
{
    public partial class ID2D1Effect
    {
        /// <summary>
        /// Sets the input by using the output of a given effect.
        /// </summary>
        /// <param name="index">Index of the input</param>
        /// <param name="effect">Effect output to use as input</param>
        /// <param name="invalidate">To invalidate</param>
        public void SetInputEffect(int index, ID2D1Effect effect, bool invalidate = true)
        {
            using (ID2D1Image output = effect.Output)
            {
                SetInput(index, output, invalidate);
            }
        }
    }
}
