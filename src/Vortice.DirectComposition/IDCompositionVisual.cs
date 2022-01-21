// Copyright © Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using System.Numerics;

namespace Vortice.DirectComposition;

public partial class IDCompositionVisual
{
    public Result SetTransform(Matrix3x2 matrix) => SetTransform(ref matrix);
}
