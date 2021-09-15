// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using System.Runtime.InteropServices;
using SharpGen.Runtime;
using Vortice.Win32;

namespace Vortice.MediaFoundation
{
    public partial class IMMDevice
    {
        private PropertyStore? _propertyStore;

        public DeviceStates State
        {
            get
            {
                GetState(out int dwState).CheckError();
                return (DeviceStates)dwState;
            }
        }

        public string Id
        {
            get
            {
                unsafe
                {
                    IntPtr id;
                    GetId(new IntPtr(&id));

                    var str = new string((char*)id);
                    Marshal.FreeCoTaskMem(id);
                    return str;
                }
            }
        }

        public PropertyStore Properties
        {
            get
            {
                if (_propertyStore == null)
                {
                    OpenPropertyStore();
                }

                return _propertyStore!;
            }
        }

        /// <summary>
        /// Friendly name for the endpoint
        /// </summary>
        public string FriendlyName
        {
            get
            {
                if (_propertyStore == null)
                {
                    OpenPropertyStore();
                }

                if (_propertyStore!.Contains(PropertyKeys.PKEY_Device_FriendlyName))
                {
                    return (string)_propertyStore.GetValue(PropertyKeys.PKEY_Device_FriendlyName).Value;
                }

                return "Unknown";
            }
        }

        /// <summary>
        /// Friendly name of device
        /// </summary>
        public string DeviceFriendlyName
        {
            get
            {
                if (_propertyStore == null)
                {
                    OpenPropertyStore();
                }

                if (_propertyStore!.Contains(PropertyKeys.PKEY_DeviceInterface_FriendlyName))
                {
                    return (string)_propertyStore.GetValue(PropertyKeys.PKEY_DeviceInterface_FriendlyName).Value;
                }

                return "Unknown";
            }
        }

        /// <summary>
        /// Icon path of device
        /// </summary>
        public string IconPath
        {
            get
            {
                if (_propertyStore == null)
                {
                    OpenPropertyStore();
                }

                if (_propertyStore!.Contains(PropertyKeys.PKEY_Device_IconPath))
                {
                    return (string)_propertyStore.GetValue(PropertyKeys.PKEY_Device_IconPath).Value;
                }

                return "Unknown";
            }
        }

        /// <summary>
        /// Device Instance Id of Device
        /// </summary>
        public string InstanceId
        {
            get
            {
                if (_propertyStore == null)
                {
                    OpenPropertyStore();
                }

                if (_propertyStore!.Contains(PropertyKeys.PKEY_Device_InstanceId))
                {
                    return (string)_propertyStore.GetValue(PropertyKeys.PKEY_Device_InstanceId).Value;
                }

                return "Unknown";
            }
        }

        /// <summary>
        /// Data Flow
        /// </summary>
        public DataFlow DataFlow
        {
            get
            {
                using (IMMEndpoint endPoint = QueryInterface<IMMEndpoint>())
                {
                    return endPoint.DataFlow;
                }
            }
        }


        public void OpenPropertyStore(StorageAccessMode access = StorageAccessMode.Read)
        {
            DisposeProperyStore();
            OpenPropertyStore((int)access, out _propertyStore);
        }

        protected override void DisposeCore(IntPtr nativePointer, bool disposing)
        {
            base.DisposeCore(nativePointer, disposing);

            DisposeProperyStore();
        }

        private void DisposeProperyStore()
        {
            if (_propertyStore != null)
            {
                _propertyStore.Dispose();
                _propertyStore = null;
            }
        }

        public override string ToString() => FriendlyName;
    }
}
