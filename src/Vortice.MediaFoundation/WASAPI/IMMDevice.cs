// Copyright (c) Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using System.Runtime.InteropServices;
using SharpGen.Runtime.Win32;
using Vortice.Win32;

namespace Vortice.MediaFoundation;

public partial class IMMDevice
{
    private IPropertyStore? _propertyStore;

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

    public IPropertyStore Properties
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

            if (_propertyStore!.TryGetValue(PropertyKeys.PKEY_Device_FriendlyName, out Variant variant))
            {
                return (string)variant.Value;
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

            if (_propertyStore!.TryGetValue(PropertyKeys.PKEY_DeviceInterface_FriendlyName, out Variant variant))
            {
                return (string)variant.Value;
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

            if (_propertyStore!.TryGetValue(PropertyKeys.PKEY_Device_IconPath, out Variant variant))
            {
                return (string)variant.Value;
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

            if (_propertyStore!.TryGetValue(PropertyKeys.PKEY_Device_InstanceId, out Variant variant))
            {
                return (string)variant.Value;
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
        DisposePropertyStore();
        OpenPropertyStore((int)access, out _propertyStore);
    }

    protected override void DisposeCore(IntPtr nativePointer, bool disposing)
    {
        base.DisposeCore(nativePointer, disposing);

        DisposePropertyStore();
    }

    private void DisposePropertyStore()
    {
        if (_propertyStore != null)
        {
            _propertyStore.Dispose();
            _propertyStore = null;
        }
    }

    public override string ToString() => FriendlyName;
}
