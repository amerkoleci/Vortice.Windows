// Copyright (c) Amer Koleci and contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using Vortice.DirectWrite;

namespace Vortice.Direct2D1;

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

    public ID2D1DrawingStateBlock1 CreateDrawingStateBlock(DrawingStateDescription1 drawingStateDescription, IDWriteRenderingParams? textRenderingParams)
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

    /// <summary>
    /// Register a <see cref="CustomEffectBase"/> factory.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="effectFactory"></param>
    public void RegisterEffect<T>(Func<T> effectFactory) where T : CustomEffectBase
    {
        Guid effectId = typeof(T).GUID;
        RegisterEffect<T>(effectFactory, effectId);
    }

    /// <summary>
    /// Register a <see cref="ID2D1EffectImpl"/> factory.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="effectFactory"></param>
    /// <param name="effectId"></param>
    public void RegisterEffect<T>(Func<T> effectFactory, Guid effectId) where T : CustomEffectBase
    {
        CustomEffectFactory factory;
        lock (_customEffectFactories)
        {
            if (_customEffectFactories.ContainsKey(effectId))
                throw new ArgumentException("An effect is already registered with this GUID", nameof(effectFactory));

            factory = new CustomEffectFactory(typeof(T), () => effectFactory());
            _customEffectFactories.Add(effectId, factory);
        }
        RegisterEffectFromString(effectId, factory.GetXML(), factory.GetBindings(), factory.Callback);
    }

    /// <summary>
    /// Register a <see cref="CustomEffectBase"/>.
    /// </summary>
    /// <typeparam name="T">Type of </typeparam>
    public void RegisterEffect<T>() where T : CustomEffectBase, new()
    {
        RegisterEffect<T>(typeof(T).GUID);
    }

    /// <summary>
    /// Register a <see cref="CustomEffectBase"/>.
    /// </summary>
    /// <typeparam name="T">Type of </typeparam>
    /// <param name="effectId"></param>
    public void RegisterEffect<T>(Guid effectId) where T : CustomEffectBase, new()
    {
        lock (_customEffectFactories)
        {
            if (_customEffectFactories.ContainsKey(effectId))
                return;

            var factory = new CustomEffectFactory(typeof(T), () => new T());
            _customEffectFactories.Add(effectId, factory);
            RegisterEffectFromString(effectId, factory.GetXML(), factory.GetBindings(), factory.Callback);
        }
    }

    public void UnregisterEffect<T>() where T : CustomEffectBase, new()
    {
        lock (_customEffectFactories)
        {
            Guid effectGuid = typeof(T).GUID;
            if (!_customEffectFactories.ContainsKey(effectGuid))
                return;

            _customEffectFactories.Remove(effectGuid);
            UnregisterEffect(effectGuid);
        }
    }
}
