// Copyright © Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using SharpGen.Runtime;
using Vortice.Multimedia;
using static Vortice.MediaFoundation.MediaEngineAttributeGuids;

namespace Vortice.MediaFoundation;

public partial class MediaEngineAttributeKeys
{
    public static readonly MediaAttributeKey<MediaEngineProtectionFlags> ContentProtectionFlags = new(ContentProtectionFlagsGuid, nameof(ContentProtectionFlags));
    public static readonly MediaAttributeKey<IMFDXGIDeviceManager> DxgiManager = new(DxgiManagerGuid, nameof(DxgiManager));
    public static readonly MediaAttributeKey<IMFMediaEngineExtension> Extension = new(ExtensionGuid, nameof(Extension));
    public static readonly MediaAttributeKey<IntPtr> OpmHwnd = new(OpmHwndGuid, nameof(OpmHwnd));
    public static readonly MediaAttributeKey<IntPtr> PlaybackHwnd = new(PlaybackHwndGuid, nameof(PlaybackHwnd));
    public static readonly MediaAttributeKey<AudioStreamCategory> AudioCategory = new(AudioCategoryGuid, nameof(AudioCategory));
    public static readonly MediaAttributeKey<AudioEndpointRole> AudioEndpointRole = new(AudioEndpointRoleGuid, nameof(AudioEndpointRole));
    public static readonly MediaAttributeKey<IUnknown> Callback = new(CallbackGuid, nameof(Callback));
    public static readonly MediaAttributeKey<IUnknown> ContentProtectionManager = new(ContentProtectionManagerGuid, nameof(ContentProtectionManager));
    public static readonly MediaAttributeKey<IUnknown> PlaybackVisual = new(PlaybackVisualGuid, nameof(PlaybackVisual));
    public static readonly MediaAttributeKey<int> VideoOutputFormat = new(VideoOutputFormatGuid, nameof(VideoOutputFormat));
}
