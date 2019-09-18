// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using SharpGen.Runtime;

namespace Vortice.DXGI
{
    public partial class IDXGIOutput5
    {
        public IDXGIOutputDuplication DuplicateOutput1(IUnknown device, params Format[] supportedFormats)
        {
            if (PlatformDetection.IsUAP)
            {
                throw new NotSupportedException("IDXGIOutput5.DuplicateOutput1 is not supported on UAP platform");
            }

            return DuplicateOutput1_(device, 0, supportedFormats.Length, supportedFormats);
        }
    }
}
