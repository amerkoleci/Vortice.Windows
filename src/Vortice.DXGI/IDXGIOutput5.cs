// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using SharpGen.Runtime;

namespace Vortice.DXGI
{
    public partial class IDXGIOutput5
    {
#if !WINDOWS_UWP
        public IDXGIOutputDuplication DuplicateOutput1(IUnknown device, Format[] supportedFormats)
        {
            return DuplicateOutput1Private(device, 0, supportedFormats.Length, supportedFormats);
        }
#endif
    }
}
