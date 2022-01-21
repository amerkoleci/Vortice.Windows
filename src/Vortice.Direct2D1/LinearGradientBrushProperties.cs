// Copyright © Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using Vortice.Mathematics;

namespace Vortice.Direct2D1;

/// <summary>
/// Contains the starting point and endpoint of the gradient axis for an <see cref="ID2D1LinearGradientBrush"/>.
/// </summary>
public partial struct LinearGradientBrushProperties
{
    /// <summary>
    /// Initializes a new instance of the <see cref="LinearGradientBrushProperties"/> struct.
    /// </summary>
    /// <param name="startPoint">In the brush's coordinate space, the starting point of the gradient axis.</param>
    /// <param name="endPoint">In the brush's coordinate space, the endpoint of the gradient axis.</param>
    public LinearGradientBrushProperties(in Point startPoint, in Point endPoint)
    {
        StartPoint = startPoint;
        EndPoint = endPoint;
    }
}
