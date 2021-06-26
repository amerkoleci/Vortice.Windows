// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

namespace Vortice.Dxc
{
    public enum DxcValidatorFlags : int
    {
        Default = 0,
        InPlaceEdit = 1,
        RootSignatureOnly = 2,
        ModuleOnly = 4,
        ValidMask = 0x7,
    }
}
