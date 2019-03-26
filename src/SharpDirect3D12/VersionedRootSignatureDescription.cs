// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using System.Runtime.InteropServices;
using SharpDXGI;

namespace SharpDirect3D12
{
    /// <summary>
    /// Holds any version of a root signature description, and is designed to be used with serialization/deserialization functions.
    /// </summary>
    public partial class VersionedRootSignatureDescription
    {
        public RootSignatureVersion Version { get; set; }
        public RootSignatureDescription Description_1_0 { get; set; }
        public RootSignatureDescription1 Description_1_1 { get; set; }

        #region Marshal
        [StructLayout(LayoutKind.Sequential, Pack = 0)]
        internal struct __Native
        {
            public RootSignatureVersion Version;
            public Union Union;

            internal void __MarshalFree()
            {
            }
        }

        [StructLayout(LayoutKind.Explicit, Pack = 0)]
        internal struct Union
        {
            [FieldOffset(0)]
            public RootSignatureDescription.__Native Desc_1_0;

            [FieldOffset(0)]
            public RootSignatureDescription1.__Native Desc_1_1;
        }

        internal unsafe void __MarshalFree(ref __Native @ref)
        {
            @ref.__MarshalFree();
        }

        internal unsafe void __MarshalFrom(ref __Native @ref)
        {
            Version = @ref.Version;
            if (Version == RootSignatureVersion.Version11)
            {
                Description_1_1.__MarshalFrom(ref @ref.Union.Desc_1_1);
            }
            else
            {
                Description_1_0.__MarshalFrom(ref @ref.Union.Desc_1_0);
            }
        }

        internal unsafe void __MarshalTo(ref __Native @ref)
        {
            @ref.Version = Version;
            if (Version == RootSignatureVersion.Version11)
            {
                Description_1_1.__MarshalTo(ref @ref.Union.Desc_1_1);
            }
            else
            {
                Description_1_0.__MarshalTo(ref @ref.Union.Desc_1_0);
            }
        }
        #endregion
    }
}
