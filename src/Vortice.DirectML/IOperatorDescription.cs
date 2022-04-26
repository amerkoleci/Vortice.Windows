// Copyright © Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.DirectML;

public interface IOperatorDescription
{
    OperatorType OperatorType { get; }
}

internal interface IOperatorDescriptionMarshal
{
    IntPtr __MarshalAlloc();
    void __MarshalFree(ref IntPtr pDesc);
}
