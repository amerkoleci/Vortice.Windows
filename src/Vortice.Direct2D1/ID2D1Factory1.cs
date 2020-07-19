// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using Vortice.DirectWrite;

namespace Vortice.Direct2D1
{
    public partial class ID2D1Factory1
    {
        private readonly Dictionary<Guid, CustomEffectFactory> _customEffectFactories = new Dictionary<Guid, CustomEffectFactory>();

        public new ID2D1DrawingStateBlock1 CreateDrawingStateBlock()
        {
            return CreateDrawingStateBlock(null, null);
        }

        public ID2D1DrawingStateBlock1 CreateDrawingStateBlock(DrawingStateDescription1 drawingStateDescription)
        {
            return CreateDrawingStateBlock(drawingStateDescription, null);
        }

        public ID2D1DrawingStateBlock1 CreateDrawingStateBlock(DrawingStateDescription1 drawingStateDescription, IDWriteRenderingParams textRenderingParams)
        {
            return CreateDrawingStateBlock(drawingStateDescription, textRenderingParams);
        }

        public ID2D1StrokeStyle1 CreateStrokeStyle(StrokeStyleProperties1 properties)
        {
            return CreateStrokeStyle(ref properties, null, 0);
        }

        public ID2D1StrokeStyle1 CreateStrokeStyle(StrokeStyleProperties1 properties, float[] dashes)
        {
            return CreateStrokeStyle(ref properties, dashes, dashes.Length);
        }

        public Guid[] GetRegisteredEffects()
        {
            Guid[] guids = Array.Empty<Guid>();
            GetRegisteredEffects(guids, 0, out _, out var count);
            guids = new Guid[count];
            GetRegisteredEffects(guids, count, out _, out _);
            return guids;
        }

        public void RegisterEffect<T>() where T : ID2D1EffectImpl, new()
        {
            lock (_customEffectFactories)
            {
                var effectGuid = typeof(T).GUID;
                if (_customEffectFactories.ContainsKey(effectGuid))
                    return;

                var factory = new CustomEffectFactory(typeof(T), () => new T());
                _customEffectFactories.Add(effectGuid, factory);
                RegisterEffectFromString(effectGuid, factory.GetXML(), factory.GetBindings(), factory.Callback);
            }
        }

        public void UnregisterEffect<T>() where T : ID2D1EffectImpl, new()
        {
            lock (_customEffectFactories)
            {
                var effectGuid = typeof(T).GUID;
                if (!_customEffectFactories.ContainsKey(effectGuid))
                    return;

                _customEffectFactories.Remove(effectGuid);
                UnregisterEffect(effectGuid);
            }
        }
    }
}
