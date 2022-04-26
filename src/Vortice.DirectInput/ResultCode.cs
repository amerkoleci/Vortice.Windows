// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using SharpGen.Runtime;
using Vortice.Win32;

namespace Vortice.DirectInput;

partial class ResultCode
{
    /// <unmanaged>DIERR_NOTFOUND</unmanaged>
    /// <unmanaged-short>DIERR_NOTFOUND</unmanaged-short>
    public static readonly Result NotFound = new((int)ErrorCode.NotFound);

    /// <unmanaged>DIERR_ALREADYINITIALIZED</unmanaged>
    /// <unmanaged-short>DIERR_ALREADYINITIALIZED</unmanaged-short>
    public static readonly Result AlreadyInitialized = new((int)ErrorCode.AlreadyInitialized);

    /// <unmanaged>DIERR_NOTINITIALIZED</unmanaged>
    /// <unmanaged-short>DIERR_NOTINITIALIZED</unmanaged-short>
    public static readonly Result NotInitialized = new((int)ErrorCode.NotReady);

    /// <unmanaged>DIERR_NOTACQUIRED</unmanaged>
    /// <unmanaged-short>DIERR_NOTACQUIRED</unmanaged-short>
    public static readonly Result NotAcquired = new((int)ErrorCode.InvalidAccess);

    /// <unmanaged>DIERR_OBJECTNOTFOUND</unmanaged>
    /// <unmanaged-short>DIERR_OBJECTNOTFOUND</unmanaged-short>
    public static readonly Result ObjectNotFound = new((int)ErrorCode.ObjectNotFound);

    /// <unmanaged>DIERR_INPUTLOST</unmanaged>
    /// <unmanaged-short>DIERR_INPUTLOST</unmanaged-short>
    public static readonly Result InputLost = new((int)ErrorCode.ReadFault);

    /// <unmanaged>DIERR_BETADIRECTINPUTVERSION</unmanaged>
    /// <unmanaged-short>DIERR_BETADIRECTINPUTVERSION</unmanaged-short>
    public static readonly Result BetaDirectInputVersion = new((int)ErrorCode.RmodeApp);

    /// <unmanaged>DIERR_BADDRIVERVER</unmanaged>
    /// <unmanaged-short>DIERR_BADDRIVERVER</unmanaged-short>
    public static readonly Result BadDriverVersion = new((int)ErrorCode.BadDriverLevel);

    /// <unmanaged>DIERR_OLDDIRECTINPUTVERSION</unmanaged>
    /// <unmanaged-short>DIERR_OLDDIRECTINPUTVERSION</unmanaged-short>
    public static readonly Result OldDirectInputVersion = new((int)ErrorCode.OldWinVersion);

    /// <unmanaged>DIERR_ACQUIRED</unmanaged>
    /// <unmanaged-short>DIERR_ACQUIRED</unmanaged-short>
    public static readonly Result Acquired = new((int)ErrorCode.Busy);
}
