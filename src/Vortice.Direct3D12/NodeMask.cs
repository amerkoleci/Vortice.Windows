// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Vortice.Direct3D12
{
    /// <summary>
    /// A state subobject that identifies the GPU nodes to which the state object applies.
    /// </summary>
    public partial struct NodeMask : IStateSubObjectDescription, IStateSubObjectDescriptionMarshal
    {
        StateSubObjectType IStateSubObjectDescription.SubObjectType => StateSubObjectType.NodeMask;

        /// <summary>
        /// Initializes a new instance of the <see cref="NodeMask"/> struct.
        /// </summary>
        /// <param name="mask">The node mask.</param>
        public NodeMask(int mask)
        {
            Mask = mask;
        }

        #region Marshal
        unsafe IntPtr IStateSubObjectDescriptionMarshal.__MarshalAlloc(Dictionary<StateSubObject, IntPtr> subObjectLookup)
        {
            var native = Marshal.AllocHGlobal(sizeof(NodeMask));
            Unsafe.Write(native.ToPointer(), Mask);
            return native;
        }

        unsafe void IStateSubObjectDescriptionMarshal.__MarshalFree(ref IntPtr pDesc)
        {
            Marshal.FreeHGlobal(pDesc);
        }
        #endregion Marshal
    }
}
