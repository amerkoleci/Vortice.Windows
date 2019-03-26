// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using System.Runtime.InteropServices;
using SharpDXGI;

namespace SharpDirect3D12
{
    /// <summary>
    /// Describes the slot of a root signature version 1.0.
    /// </summary>
    public partial struct RootParameter
    {
        #region Marshal
        private RootParameterType _parameterType;
        private Union _union;
        private ShaderVisibility _shaderVisibility;

        [StructLayout(LayoutKind.Explicit, Pack = 0)]
        private struct Union
        {
            [FieldOffset(0)]
            public RootDescriptorTable.__Native DescriptorTable;

            [FieldOffset(0)]
            public RootConstants Constants;

            [FieldOffset(0)]
            public RootDescriptor Descriptor;
        }
        #endregion
    }
}
