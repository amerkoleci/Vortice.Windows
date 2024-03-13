// Copyright (c) Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.XAudio2;

/// <summary>
/// EventArgs used by <see cref="IXAudio2.CriticalError"/>.
/// </summary>
public class ErrorEventArgs : EventArgs
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ErrorEventArgs"/> class.
    /// </summary>
    /// <param name="errorCode">The error code.</param>
    public ErrorEventArgs(Result errorCode)
    {
        ErrorCode = errorCode;
    }

    /// <summary>
    /// Gets the error code.
    /// </summary>
    public Result ErrorCode { get; }
}
