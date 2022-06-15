// Copyright © Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using SharpGen.Runtime;
using Vortice.Multimedia;

namespace Vortice.MediaFoundation;

public partial class MediaEngineAttributeKeys
{
    public static readonly MediaAttributeKey<MediaEngineProtectionFlags> ContentProtectionFlags = new(ContentProtectionFlagsGuid, "ContentProtectionFlags");
    public static readonly MediaAttributeKey<IMFDXGIDeviceManager> DxgiManager = new(DxgiManagerGuid, "DxgiManager");
    public static readonly MediaAttributeKey<IMFMediaEngineExtension> Extension = new(ExtensionGuid, "Extension");
    public static readonly MediaAttributeKey<IntPtr> OpmHwnd = new(OpmHwndGuid, "OpmHwnd");
    public static readonly MediaAttributeKey<IntPtr> PlaybackHwnd = new(PlaybackHwndGuid, "PlaybackHwnd");
    public static readonly MediaAttributeKey<AudioEndpointRole> AudioEndpointRole = new(AudioEndpointRoleGuid);
    public static readonly MediaAttributeKey<ComObject> Callback = new(CallbackGuid);
    public static readonly MediaAttributeKey<ComObject> ContentProtectionManager = new(ContentProtectionManagerGuid);
    public static readonly MediaAttributeKey<ComObject> PlaybackVisual = new(PlaybackVisualGuid);
    public static readonly MediaAttributeKey<int> VideoOutputFormat = new(VideoOutputFormatGuid);
}
