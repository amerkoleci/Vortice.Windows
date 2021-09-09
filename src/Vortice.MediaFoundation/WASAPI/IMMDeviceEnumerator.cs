// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System.Collections.Generic;
using System.Runtime.InteropServices;
using SharpGen.Runtime;

namespace Vortice.MediaFoundation
{
    public partial class IMMDeviceEnumerator
    {
        public IMMDeviceEnumerator()
        {
            ComUtilities.CreateComInstance(typeof(MMDeviceEnumeratorComObject).GUID, ComContext.InprocServer, typeof(IMMDeviceEnumerator).GUID, this);
        }

        public IMMDevice GetDefaultAudioEndpoint(DataFlow dataFlow, Role role)
        {
            GetDefaultAudioEndpoint(dataFlow, role, out IMMDevice endPoint).CheckError();
            return endPoint;
        }

        public IMMDevice GetDevice(string pwstrId)
        {
            GetDevice(pwstrId, out IMMDevice device).CheckError();
            return device;
        }

        public IReadOnlyList<IMMDevice> EnumAudioEndpoints(DataFlow dataFlow, DeviceStates states = DeviceStates.Active)
        {
            EnumAudioEndpoints(dataFlow, (int)states, out IMMDeviceCollection collection).CheckError();
            List<IMMDevice> result = new List<IMMDevice>(collection.Count);
            for (int i = 0; i < collection.Count; i++)
            {
                result.Add(collection.Item(i));
            }

            collection.Dispose();
            return result;
        }

        [ComImport, Guid("BCDE0395-E52F-467C-8E3D-C4579291692E")]
        class MMDeviceEnumeratorComObject
        {
        }
    }
}
