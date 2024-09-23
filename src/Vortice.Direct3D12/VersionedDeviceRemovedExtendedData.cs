// Copyright (c) Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.Direct3D12;

/// <summary>
/// Represents versioned Device Removed Extended Data (DRED) data, so that debuggers and debugger extensions can access DRED data.
/// </summary>
public partial struct VersionedDeviceRemovedExtendedData
{
    /// <summary>
    /// Specifies the barrier type, see <see cref="ResourceBarrierType"/>
    /// </summary>
    public DredVersion Version;

    private Union _union;

    public DeviceRemovedExtendedData Dred_1_0
    {
        get => _union.Dred_1_0;
        set => _union.Dred_1_0 = value;
    }

    public DeviceRemovedExtendedData1 Dred_1_1
    {
        get => _union.Dred_1_1;
        set => _union.Dred_1_1 = value;
    }

    [StructLayout(LayoutKind.Explicit, Pack = 0)]
    private struct Union
    {
        [FieldOffset(0)]
        public DeviceRemovedExtendedData Dred_1_0;

        [FieldOffset(0)]
        public DeviceRemovedExtendedData1 Dred_1_1;
    }
}
