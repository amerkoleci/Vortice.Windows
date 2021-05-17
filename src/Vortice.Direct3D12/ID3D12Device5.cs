// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using SharpGen.Runtime;
using Vortice.Direct3D;

namespace Vortice.Direct3D12
{
    public partial class ID3D12Device5
    {
        /// <summary>
        /// Creates an instance of the specified meta command.
        /// </summary>
        /// <param name="commandId">A <see cref="Guid"/> of the meta command that you wish to instantiate.</param>
        /// <returns>An instance of <see cref="ID3D12MetaCommand"/>.</returns>
        public T? CreateMetaCommand<T>(Guid commandId) where T : ID3D12MetaCommand
        {
            Result result = CreateMetaCommand(commandId, 0, IntPtr.Zero, 0, typeof(T).GUID, out IntPtr nativePtr);
            if (result.Failure)
                return default;

            return MarshallingHelpers.FromPointer<T>(nativePtr);
        }

        /// <summary>
        /// Creates an instance of the specified meta command.
        /// </summary>
        /// <param name="commandId">A <see cref="Guid"/> of the meta command that you wish to instantiate.</param>
        /// <param name="nodeMask">For single-adapter operation, set this to zero. If there are multiple adapter nodes, set a bit to identify the node (one of the device's physical adapters) to which the meta command applies. 
        /// Each bit in the mask corresponds to a single node. Only one bit must be set. </param>
        /// <returns>An instance of <see cref="ID3D12MetaCommand"/>.</returns>
        public T? CreateMetaCommand<T>(Guid commandId, int nodeMask) where T : ID3D12MetaCommand
        {
            Result result = CreateMetaCommand(commandId, nodeMask, IntPtr.Zero, 0, typeof(T).GUID, out IntPtr nativePtr);
            if (result.Failure)
                return default;

            return MarshallingHelpers.FromPointer<T>(nativePtr);
        }

        /// <summary>
        /// Creates an instance of the specified meta command.
        /// </summary>
        /// <param name="commandId">A <see cref="Guid"/> of the meta command that you wish to instantiate.</param>
        /// <param name="blob">Blob containing the values of the parameters for creating the meta command.</param>
        /// <returns>An instance of <see cref="ID3D12MetaCommand"/>.</returns>
        public T? CreateMetaCommand<T>(Guid commandId, Blob blob) where T : ID3D12MetaCommand
        {
            Result result = CreateMetaCommand(commandId, 0, blob.BufferPointer, blob.BufferSize, typeof(T).GUID, out IntPtr nativePtr);
            if (result.Failure)
                return default;

            return MarshallingHelpers.FromPointer<T>(nativePtr);
        }

        /// <summary>
        /// Creates an instance of the specified meta command.
        /// </summary>
        /// <param name="commandId">A <see cref="Guid"/> of the meta command that you wish to instantiate.</param>
        /// <param name="nodeMask">For single-adapter operation, set this to zero. If there are multiple adapter nodes, set a bit to identify the node (one of the device's physical adapters) to which the meta command applies. 
        /// Each bit in the mask corresponds to a single node. Only one bit must be set. </param>
        /// <param name="blob">Blob containing the values of the parameters for creating the meta command.</param>
        /// <returns>An instance of <see cref="ID3D12MetaCommand"/>.</returns>
        public T? CreateMetaCommand<T>(Guid commandId, int nodeMask, Blob blob) where T : ID3D12MetaCommand
        {
            Result result = CreateMetaCommand(commandId, nodeMask, blob.BufferPointer, blob.BufferSize, typeof(T).GUID, out IntPtr nativePtr);
            if (result.Failure)
                return default;

            return MarshallingHelpers.FromPointer<T>(nativePtr);
        }

        /// <summary>
        /// Creates a <see cref="ID3D12StateObject"/>.
        /// </summary>
        /// <param name="description">The description of the state object to create.</param>
        /// <returns>An instance of <see cref="ID3D12StateObject"/>.</returns>
        public T? CreateStateObject<T>(StateObjectDescription description) where T : ID3D12StateObject
        {
            Result result = CreateStateObject(description, typeof(T).GUID, out IntPtr nativePtr);
            if (result.Failure)
                return default;

            return MarshallingHelpers.FromPointer<T>(nativePtr);
        }
    }
}
