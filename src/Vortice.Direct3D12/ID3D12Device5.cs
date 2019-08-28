// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using Vortice.DirectX.Direct3D;

namespace Vortice.Direct3D12
{
    public partial class ID3D12Device5
    {
        /// <summary>
        /// Creates an instance of the specified meta command.
        /// </summary>
        /// <param name="commandId">A <see cref="Guid"/> of the meta command that you wish to instantiate.</param>
        /// <returns>An instance of <see cref="ID3D12MetaCommand"/>.</returns>
        public ID3D12MetaCommand CreateMetaCommand(Guid commandId)
        {
            return CreateMetaCommand(commandId, 0, IntPtr.Zero, 0, typeof(ID3D12MetaCommand).GUID);
        }

        /// <summary>
        /// Creates an instance of the specified meta command.
        /// </summary>
        /// <param name="commandId">A <see cref="Guid"/> of the meta command that you wish to instantiate.</param>
        /// <param name="nodeMask">For single-adapter operation, set this to zero. If there are multiple adapter nodes, set a bit to identify the node (one of the device's physical adapters) to which the meta command applies. 
        /// Each bit in the mask corresponds to a single node. Only one bit must be set. </param>
        /// <returns>An instance of <see cref="ID3D12MetaCommand"/>.</returns>
        public ID3D12MetaCommand CreateMetaCommand(Guid commandId, int nodeMask)
        {
            return CreateMetaCommand(commandId, nodeMask, IntPtr.Zero, 0, typeof(ID3D12MetaCommand).GUID);
        }

        /// <summary>
        /// Creates an instance of the specified meta command.
        /// </summary>
        /// <param name="commandId">A <see cref="Guid"/> of the meta command that you wish to instantiate.</param>
        /// <param name="blob">Blob containing the values of the parameters for creating the meta command.</param>
        /// <returns>An instance of <see cref="ID3D12MetaCommand"/>.</returns>
        public ID3D12MetaCommand CreateMetaCommand(Guid commandId, Blob blob)
        {
            Guard.NotNull(blob, nameof(blob));

            return CreateMetaCommand(commandId, 0, blob.BufferPointer, blob.BufferSize, typeof(ID3D12MetaCommand).GUID);
        }

        /// <summary>
        /// Creates an instance of the specified meta command.
        /// </summary>
        /// <param name="commandId">A <see cref="Guid"/> of the meta command that you wish to instantiate.</param>
        /// <param name="nodeMask">For single-adapter operation, set this to zero. If there are multiple adapter nodes, set a bit to identify the node (one of the device's physical adapters) to which the meta command applies. 
        /// Each bit in the mask corresponds to a single node. Only one bit must be set. </param>
        /// <param name="blob">Blob containing the values of the parameters for creating the meta command.</param>
        /// <returns>An instance of <see cref="ID3D12MetaCommand"/>.</returns>
        public ID3D12MetaCommand CreateMetaCommand(Guid commandId, int nodeMask, Blob blob)
        {
            Guard.NotNull(blob, nameof(blob));

            return CreateMetaCommand(commandId, nodeMask, blob.BufferPointer, blob.BufferSize, typeof(ID3D12MetaCommand).GUID);
        }

        /// <summary>
        /// Creates a <see cref="ID3D12StateObject"/>.
        /// </summary>
        /// <param name="description">The description of the state object to create.</param>
        /// <returns>An instance of <see cref="ID3D12StateObject"/>.</returns>
        public ID3D12StateObject CreateStateObject(StateObjectDescription description)
        {
            return CreateStateObject(description, typeof(ID3D12StateObject).GUID);
        }
    }
}
