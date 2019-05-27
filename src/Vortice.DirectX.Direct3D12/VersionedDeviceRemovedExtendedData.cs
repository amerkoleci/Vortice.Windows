// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System.Runtime.InteropServices;

namespace Vortice.DirectX.Direct3D12
{
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
} 
