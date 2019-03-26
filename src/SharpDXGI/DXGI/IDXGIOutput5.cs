// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using SharpGen.Runtime;

namespace SharpDXGI
{
    public partial class IDXGIOutput5
    {
#if !WINDOWS_UWP
        public IDXGIOutputDuplication DuplicateOutput1(IUnknown device, Format[] supportedFormats)
        {
            Guard.NotNull(device, nameof(device));
            Guard.NotNullOrEmpty(supportedFormats, nameof(device));

            return DuplicateOutput1Private(device, 0, supportedFormats.Length, supportedFormats);
        }
#endif
    }
}
