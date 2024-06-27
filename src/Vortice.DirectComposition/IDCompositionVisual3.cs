// Copyright (c) Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using System.Numerics;

namespace Vortice.DirectComposition;

public partial class IDCompositionVisual3
{
    public Result SetTransform(Matrix4x4 matrix) => SetTransform(ref matrix);
}
