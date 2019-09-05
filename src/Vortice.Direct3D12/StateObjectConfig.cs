// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using System.Runtime.InteropServices;
using SharpGen.Runtime;
using Vortice.DirectX;

namespace Vortice.Direct3D12
{
    /// <summary>
    /// Defines general properties of a state object.
    /// </summary>
    public partial struct StateObjectConfig
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StateObjectConfig"/> struct.
        /// </summary>
        /// <param name="flags">A value from the <see cref="StateObjectFlags"/> enumeration that specifies the requirements for the state object.</param>
        public StateObjectConfig(StateObjectFlags flags)
        {
            Flags = flags;
        }
    }
}
