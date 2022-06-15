// Copyright © Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using Vortice.Multimedia;

namespace Vortice.MediaFoundation;

public partial class MediaAttributeKeys
{
    public static readonly MediaAttributeKey<byte[]> UserDataPayload = new(UserDataPayloadGuid);
}
