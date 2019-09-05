// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using System.Runtime.InteropServices;
using Vortice.DirectX;

namespace Vortice.Direct3D12
{
    /// <summary>
    /// Description of a state object. Pass a value of this structure type <see cref="ID3D12Device5.CreateStateObject(StateObjectDescription)"/>.
    /// </summary>
    public partial class StateObjectDescription
    {
        public StateObjectDescription(StateObjectType type, params StateSubObject[] subObjects)
        {
            Type = type;
            SubObjects = subObjects;
        }

        /// <summary>
        /// The type of the state object.
        /// </summary>
        public StateObjectType Type { get; }

        /// <summary>	
        /// An array of <see cref="InputElementDescription"/> that describe the data types of the input-assembler stage.
        /// </summary>	
        public StateSubObject[] SubObjects { get; }

        #region Marshal
        [StructLayout(LayoutKind.Sequential, Pack = 0)]
        internal unsafe struct __Native
        {
            public StateObjectType Type;
            public int NumSubobjects;
            public IntPtr pSubobjects;
        }

        internal unsafe void __MarshalFree(ref __Native @ref)
        {
            if (@ref.pSubobjects != null)
            {
                for (int i = 0; i < @ref.NumSubobjects; i++)
                {
                    Marshal.FreeHGlobal(SubObjects[i].Description);
                }

                Marshal.FreeHGlobal(@ref.pSubobjects);
            }
        }

        internal unsafe void __MarshalTo(ref __Native @ref)
        {
            @ref.Type = Type;
            @ref.NumSubobjects = SubObjects?.Length ?? 0;
            if (@ref.NumSubobjects > 0)
            {
                @ref.pSubobjects = Interop.AllocToPointer(SubObjects);
            }
        }
        #endregion
    }
}
