// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using SharpGen.Runtime;

namespace Vortice.MediaFoundation
{
    public partial class IMFPresentationDescriptor
    {
        public unsafe IMFStreamDescriptor GetStreamDescriptorByIndex(int index, out SharpGen.Runtime.RawBool pfSelected)
        {
            GetStreamDescriptorByIndex(index, out pfSelected, out IMFStreamDescriptor descriptor);

            return descriptor;
        }
    }
}
