// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System.IO;
using SharpGen.Runtime;

namespace Vortice.MediaFoundation
{
    public partial class IMMDevice
    {
        private const int STGM_READ = 0x00000000;
        private const int STGM_WRITE = 0x00000001;
        private const int STGM_READWRITE = 0x00000002;

        public DeviceStates State
        {
            get
            {
                GetState(out int dwState).CheckError();
                return (DeviceStates)dwState;
            }
        }

        public Result OpenPropertyStore(FileAccess access, out ComObject properties)
        {
            return OpenPropertyStore(ToCOMAccess(access), out properties);
        }

        public ComObject OpenPropertyStore(FileAccess access)
        {
            OpenPropertyStore(ToCOMAccess(access), out ComObject properties).CheckError();
            return properties;
        }

        private static int ToCOMAccess(FileAccess access)
        {
            switch (access)
            {
                default:
                case FileAccess.Read:
                    return STGM_READ;
                case FileAccess.Write:
                    return STGM_WRITE;
                case FileAccess.ReadWrite:
                    return STGM_READWRITE;
            }
        }
    }
}
