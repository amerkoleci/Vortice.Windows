// Copyright (c) Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.Dxc;

public enum DxcValidatorFlags : int
{
    Default = 0,
    InPlaceEdit = 1,
    RootSignatureOnly = 2,
    ModuleOnly = 4,
    ValidMask = 0x7,
}
