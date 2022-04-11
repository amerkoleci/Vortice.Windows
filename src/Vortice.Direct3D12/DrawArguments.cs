// Copyright © Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.Direct3D12;

/// <summary>
/// Describes parameters for drawing instances.
/// </summary>
public partial struct DrawArguments
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DrawArguments"/> struct.
    /// </summary>
    /// <param name="vertexCountPerInstance">Specifies the number of vertices to draw, per instance.</param>
    /// <param name="instanceCount">Specifies the number of instances.</param>
    /// <param name="startVertexLocation">Specifies an index to the first vertex to start drawing from.</param>
    /// <param name="startInstanceLocation">Specifies an index to the first instance to start drawing from.</param>
    public DrawArguments(int vertexCountPerInstance, int instanceCount, int startVertexLocation, int startInstanceLocation)
    {
        VertexCountPerInstance = vertexCountPerInstance;
        InstanceCount = instanceCount;
        StartVertexLocation = startVertexLocation;
        StartInstanceLocation = startInstanceLocation;
    }
}
