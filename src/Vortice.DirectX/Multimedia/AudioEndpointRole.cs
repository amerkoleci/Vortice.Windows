// Copyright © Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.Multimedia;

/// <summary>
/// This enumeration defines constants that indicate the role that the system has assigned to an audio endpoint device.
/// </summary>
/// <msdn-id>dd370842</msdn-id>	
/// <unmanaged>ERole</unmanaged>	
/// <unmanaged-short>ERole</unmanaged-short>	
public enum AudioEndpointRole
{
    /// <summary>
    /// Games, system notification sounds, and voice commands
    /// </summary>
    Console,

    /// <summary>
    /// Music, movies, narration, and live music recording.
    /// </summary>
    Multimedia,

    /// <summary>
    /// Voice communications (talking to another person).
    /// </summary>
    Communications,
}
