// Copyright © Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.Direct2D1;

public partial class ID2D1Device
{
    public ID2D1DeviceContext CreateDeviceContext() => CreateDeviceContext(DeviceContextOptions.None);
}
