// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;

namespace Vortice.Direct3D12
{
    public partial struct ResourceAliasingBarrier
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ResourceAliasingBarrier"/> struct.
        /// </summary>
        /// <param name="resourceBefore">The resource before.</param>
        /// <param name="resourceAfter">The resource after.</param>
        /// <exception cref="System.ArgumentNullException">resourceBefore</exception>
        public ResourceAliasingBarrier(ID3D12Resource resourceBefore, ID3D12Resource resourceAfter)
        {
            Guard.NotNull(resourceBefore, nameof(resourceBefore));
            Guard.NotNull(resourceAfter, nameof(resourceAfter));

            ResourceBeforePointer = resourceBefore.NativePointer;
            ResourceAfterPointer = resourceAfter.NativePointer;
        }
    }
}
