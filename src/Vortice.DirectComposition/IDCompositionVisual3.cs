// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System.Numerics;
using SharpGen.Runtime;

namespace Vortice.DirectComposition
{
    public partial class IDCompositionVisual3
    {
        public Result SetTransform(Matrix4x4 matrix) => SetTransform(ref matrix);
    }
}
