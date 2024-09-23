// Copyright (c) Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.Direct3D12.Video;

/// <summary>
/// Specifies transform parameters for video processing. Used by the <see cref="VideoProcessInputStreamArguments"/> structure.
/// </summary>
public partial struct VideoProcessTransform
{
    /// <summary>
    /// Initializes a new instance of the <see cref="VideoProcessTransform"/> struct.
    /// </summary>
    /// <param name="sourceRectangle">
    /// Specifies the source rectangle of the transform.
    /// This is the portion of the input surface that is blitted to the destination surface.
    /// The source rectangle is given in pixel coordinates, relative to the input surface.
    /// </param>
    /// <param name="destinationRectangle">
    /// Specifies the destination rectangle of the transform.
    /// This is the portion of the output surface that receives the blit for this stream.
    /// The destination rectangle is given in pixel coordinates, relative to the output surface.
    /// </param>
    /// <param name="orientation">
    /// The rotation and flip operation to apply to the source. Source and Destination rectangles are specified in post orientation coordinates.
    /// </param>
    public VideoProcessTransform(RawRect sourceRectangle, RawRect destinationRectangle, VideoProcessOrientation orientation)
    {
        SourceRectangle = sourceRectangle;
        DestinationRectangle = destinationRectangle;
        Orientation = orientation;
    }
}
