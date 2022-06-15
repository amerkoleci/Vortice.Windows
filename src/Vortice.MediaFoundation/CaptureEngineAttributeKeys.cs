// Copyright © Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using SharpGen.Runtime;

namespace Vortice.MediaFoundation;

public partial class CaptureEngineAttributeKeys
{
    public static readonly MediaAttributeKey<IMFDXGIDeviceManager> D3DManager = new(D3DManagerGuid);
    public static readonly MediaAttributeKey<ComObject> DecoderTransformFieldOfUseUnlockAttribute = new(DecoderTransformFieldOfUseUnlockAttributeGuid);
    public static readonly MediaAttributeKey<bool> DisableDXVA = new(DisableDXVAGuid);
    public static readonly MediaAttributeKey<bool> DisableHardwareTransforms = new(DisableHardwareTransformsGuid);
    //public static readonly MediaAttributeKey<bool> DisableLowLatency = new(DisableLowLatencyGuid);
    public static readonly MediaAttributeKey<ComObject> EncoderTransformFieldOfUseUnlockAttribute = new(EncoderTransformFieldOfUseUnlockAttributeGuid);
    public static readonly MediaAttributeKey<Guid> EventGenerator = new(EventGeneratorGuid);
    public static readonly MediaAttributeKey<int> EventStreamIndex = new(EventStreamIndexGuid);
    public static readonly MediaAttributeKey<ComObject> MediaSourceConfig = new(MediaSourceConfigGuid);
}
