// Copyright (c) Amer Koleci and contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.Direct3D12;

/// <summary>
/// Describes the access to resource(s) that is requested by an application at the transition into a render pass.
/// </summary>
public partial struct RenderPassBeginningAccess
{
    /// <summary>
    /// Initialize new instance of <see cref="RenderPassBeginningAccess"/> struct.
    /// </summary>
    /// <param name="type">The type of access being requested.</param>
    public RenderPassBeginningAccess(RenderPassBeginningAccessType type)
    {
        Type = type;
        Clear = default;
    }

    /// <summary>
    /// Initialize new instance of <see cref="RenderPassBeginningAccess"/> struct with <see cref="RenderPassBeginningAccessType.Clear"/> type.
    /// </summary>
    /// <param name="clear">The clear value to which resource(s) should be cleared.</param>
    public RenderPassBeginningAccess(in RenderPassBeginningAccessClearParameters clear)
    {
        Type = RenderPassBeginningAccessType.Clear;
        Clear = clear;
    }

    /// <summary>
    /// Initialize new instance of <see cref="RenderPassBeginningAccess"/> struct with <see cref="RenderPassBeginningAccessType.Clear"/> type.
    /// </summary>
    /// <param name="clearValue">The clear value to which resource(s) should be cleared.</param>
    public RenderPassBeginningAccess(in ClearValue clearValue)
    {
        Type = RenderPassBeginningAccessType.Clear;
        Clear = new RenderPassBeginningAccessClearParameters(clearValue);
    }
}
