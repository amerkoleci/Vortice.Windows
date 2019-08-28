// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using SharpGen.Runtime;

namespace Vortice.DXGI
{
    public partial class IDXGIOutput1
    {
        public void FindClosestMatchingMode1(ComObject concernedDevice, ModeDescription1 modeToMatch, out ModeDescription1 closestMatch)
        {
            FindClosestMatchingMode1(ref modeToMatch, out closestMatch, concernedDevice);
        }

        public ModeDescription1[] GetDisplayModeList1(Format format, DisplayModeEnumerationFlags flags)
        {
            int count = 0;
            GetDisplayModeList1(format, (int)flags, ref count, null);
            var result = new ModeDescription1[count];
            if (count > 0)
            {
                GetDisplayModeList1(format, (int)flags, ref count, result);
            }

            return result;
        }

#if !WINDOWS_UWP
        public IDXGIOutputDuplication DuplicateOutput(IUnknown device)
        {
            return DuplicateOutputPrivate(device);
        }
#endif
    }
}
