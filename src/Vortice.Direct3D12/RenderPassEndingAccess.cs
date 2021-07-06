// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System.Runtime.InteropServices;

namespace Vortice.Direct3D12
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
        /// Initialize new instance of <see cref="RenderPassEndingAccess"/> struct with <see cref="RenderPassEndingAccessType.Resolve"/> type.
        /// </summary>
        /// <param name="resolve">Description of the resource to resolve to.</param>
        public RenderPassEndingAccess(RenderPassEndingAccessResolveParameters resolve)
        {
            Type = RenderPassEndingAccessType.Resolve;
            Resolve = resolve;
        }

        #region Marshal
        [StructLayout(LayoutKind.Sequential, Pack = 0, CharSet = CharSet.Unicode)]
        internal partial struct __Native
        {
            public RenderPassEndingAccessType Type;
            public RenderPassEndingAccessResolveParameters.__Native Resolve;
        }

        internal void __MarshalTo(ref __Native @ref)
        {
            @ref.Type = Type;
            Resolve?.__MarshalTo(ref @ref.Resolve);
        }

        internal void __MarshalFrom(ref __Native @ref)
        {
            Type = @ref.Type;
            Resolve = new RenderPassEndingAccessResolveParameters();
            Resolve.__MarshalFrom(ref @ref.Resolve);
        }

        internal void __MarshalFree(ref __Native @ref)
        {
            Resolve?.__MarshalFree(ref @ref.Resolve);
        }
        #endregion Marshal
    }
}
