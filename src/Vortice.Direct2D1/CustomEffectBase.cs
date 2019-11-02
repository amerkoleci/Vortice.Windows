using System;
using System.Collections.Generic;
using System.Text;
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
