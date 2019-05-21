// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using System.Runtime.CompilerServices;
using Vortice.DirectX.DXGI;
using SharpGen.Runtime;

namespace Vortice.DirectX.Direct3D12
{
    /// <summary>
    /// Describes the access to resource(s) that is requested by an application at the transition out of a render pass.
    /// </summary>
    public partial struct RenderPassEndingAccess
    {
        /// <summary>
        /// Initialize new instance of <see cref="RenderPassEndingAccess"/> struct.
        /// </summary>
        /// <param name="type">The type of access being requested.</param>
        public RenderPassEndingAccess(RenderPassEndingAccessType type)
        {
            Type = type;
            Resolve = default;
        }

        /// <summary>
        /// Initialize new instance of <see cref="RenderPassEndingAccess"/> struct.
        /// </summary>
        /// <param name="type">The type of access being requested.</param>
        /// <param name="resolve">Appropriate when Type is <see cref="RenderPassEndingAccessType.Resolve"/>. Description of the resource to resolve to.</param>
        public RenderPassEndingAccess(
            RenderPassEndingAccessType type,
            RenderPassEndingAccessResolveParameters resolve)
        {
            Type = type;
            Resolve = resolve;
        }
    }
}
