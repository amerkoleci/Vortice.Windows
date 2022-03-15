// Copyright © Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.DirectStorage;

public partial struct ErrorFirstFailure
{
    /// <summary>
    /// The <see cref="Result"/> code of the failure.
    /// </summary>
    public readonly Result HResult;

    /// <summary>
    /// Type of the Enqueue command that caused the failure.
    /// </summary>
    public readonly CommandType CommandType;

    private Union _union;

    /// <summary>
    /// The parameters passed to the Enqueue call.
    /// </summary>
    public ErrorParametersRequest Request
    {
        get
        {
            var result = new ErrorParametersRequest();
            result.__MarshalFrom(ref _union.Request);
            return result;
        }
    }

    /// <summary>
    /// The parameters passed to the Enqueue call.
    /// </summary>
    public ErrorParametersStatus Status
    {
        get
        {
            var result = new ErrorParametersStatus();
            result.__MarshalFrom(ref _union.Status);
            return result;
        }
    }

    /// <summary>
    /// The parameters passed to the Enqueue call.
    /// </summary>
    public ErrorParametersSignal Signal
    {
        get
        {
            var result = new ErrorParametersSignal();
            result.__MarshalFrom(ref _union.Signal);
            return result;
        }
    }

    #region Nested
    [StructLayout(LayoutKind.Explicit, Pack = 0)]
    private struct Union
    {
        [FieldOffset(0)]
        public ErrorParametersRequest.__Native Request;

        [FieldOffset(0)]
        public ErrorParametersStatus.__Native Status;

        [FieldOffset(0)]
        public ErrorParametersSignal.__Native Signal;
    }
    #endregion
}
