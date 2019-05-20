// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System.Runtime.InteropServices;

namespace Vortice.DirectX.Direct3D12
{
    /// <summary>
    /// Describes the slot of a root signature version 1.0.
    /// </summary>
    public partial struct RootParameter1
    {
        public RootParameterType ParameterType;
        private Union _union;

        public RootDescriptorTable1 DescriptorTable
        {
            get
            {
                var result = new RootDescriptorTable1();
                result.__MarshalFrom(ref _union.DescriptorTable);
                return result;
            }
            set
            {
                value.__MarshalTo(ref _union.DescriptorTable);
            }
        }

        public RootConstants Constants
        {
            get => _union.Constants;
            set => _union.Constants = value;
        }

        public RootDescriptor1 Descriptor
        {
            get => _union.Descriptor;
            set => _union.Descriptor = value;
        }

        public ShaderVisibility ShaderVisibility;

        [StructLayout(LayoutKind.Explicit, Pack = 0)]
        private struct Union
        {
            [FieldOffset(0)]
            public RootDescriptorTable1.__Native DescriptorTable;

            [FieldOffset(0)]
            public RootConstants Constants;

            [FieldOffset(0)]
            public RootDescriptor1 Descriptor;
        }
    }
}
