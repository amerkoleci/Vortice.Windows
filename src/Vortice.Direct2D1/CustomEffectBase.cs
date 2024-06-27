// Copyright (c) Amer Koleci and contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.Direct2D1;

public abstract class CustomEffectBase : CallbackBase, ID2D1EffectImpl
{
    /// <inheritdoc/>
    public virtual void Initialize(ID2D1EffectContext effectContext, ID2D1TransformGraph transformGraph)
    {
    }

    /// <inheritdoc/>
    public virtual void PrepareForRender(ChangeType changeType)
    {
    }

    /// <inheritdoc/>
    public virtual void SetGraph(ID2D1TransformGraph transformGraph)
    {
    }
}
