// Copyright © Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.DXGI;

public unsafe partial struct DisplayColorSpace
{
    public fixed float PrimaryCoordinates[8 * 2];

    public fixed float WhitePoints[16 * 2];
}
