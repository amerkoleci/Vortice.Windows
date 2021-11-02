// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using System.Runtime.InteropServices;
using SharpGen.Runtime;

namespace Vortice.DXGI
{
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public partial struct AdapterDescription3
    {
        public string Description;

        public uint VendorId;

        public uint DeviceId;

        public uint SubsystemId;

        public uint Revision;

        public nuint DedicatedVideoMemory;

        public nuint DedicatedSystemMemory;

        public nuint SharedSystemMemory;

        public long AdapterLuid;

        public AdapterFlags3 Flags;
        public GraphicsPreemptionGranularity GraphicsPreemptionGranularity;
        public ComputePreemptionGranularity ComputePreemptionGranularity;

        #region Marshal
        [StructLayout(LayoutKind.Sequential, Pack = 4)]
        internal unsafe struct __Native
        {
            public fixed ushort Description[128];
            public uint VendorId;
            public uint DeviceId;
            public uint SubSysId;
            public uint Revision;
            public nuint DedicatedVideoMemory;
            public nuint DedicatedSystemMemory;
            public nuint SharedSystemMemory;
            public long AdapterLuid;
            public AdapterFlags3 Flags;
            public GraphicsPreemptionGranularity GraphicsPreemptionGranularity;
            public ComputePreemptionGranularity ComputePreemptionGranularity;
        }

        internal unsafe void __MarshalFrom(ref __Native @ref)
        {
            fixed (ushort* ptr = &@ref.Description[0])
            {
                Description = StringHelpers.PtrToStringUni((IntPtr)ptr, 128);
            }

            VendorId = @ref.VendorId;
            DeviceId = @ref.DeviceId;
            SubsystemId = @ref.SubSysId;
            Revision = @ref.Revision;
            DedicatedVideoMemory = @ref.DedicatedVideoMemory;
            DedicatedSystemMemory = @ref.DedicatedSystemMemory;
            SharedSystemMemory = @ref.SharedSystemMemory;
            AdapterLuid = @ref.AdapterLuid;
            Flags = @ref.Flags;
            GraphicsPreemptionGranularity = @ref.GraphicsPreemptionGranularity;
            ComputePreemptionGranularity = @ref.ComputePreemptionGranularity;
        }
        #endregion
    }
}
