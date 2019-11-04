// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using SharpGen.Runtime;

namespace Vortice.Direct2D1
{
    public abstract class CustomEffectBase : CallbackBase, ID2D1EffectImpl
    {
        public abstract void Initialize(ID2D1EffectContext effectContext, ID2D1TransformGraph transformGraph);

        public abstract void PrepareForRender(ChangeType changeType);

        public abstract void SetGraph(ID2D1TransformGraph transformGraph);
    }
}
