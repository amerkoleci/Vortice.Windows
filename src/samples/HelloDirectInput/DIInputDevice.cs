// Copyright © Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using System.Diagnostics;
using SharpGen.Runtime;
using Vortice.DirectInput;

namespace HelloDirectInput;

public sealed class DIInputDevice
{
    private readonly IDirectInput8 _directInput;

    private Dictionary<Guid, DeviceInstance> _keyboards;
    private Dictionary<Guid, DeviceInstance> _joysticks;

    private Dictionary<Guid, IDirectInputDevice8> _keyboardDevices;
    private Dictionary<Guid, IDirectInputDevice8> _joystickDevices;

    public DIInputDevice()
    {
        _directInput = DInput.DirectInput8Create();

        _keyboardDevices = new Dictionary<Guid, IDirectInputDevice8>();
        _joystickDevices = new Dictionary<Guid, IDirectInputDevice8>();
    }

    public void Initialize(IntPtr handle)
    {
        try
        {
            _keyboards = EnumerateAllAttachedKeyboardDevices(_directInput);

            foreach (Guid item in _keyboards.Keys)
            {
                InitializeKeyboardDevice(_directInput, item, handle);
            }

            _joysticks = EnumerateAllAttachedGameDevices(_directInput);

            foreach (Guid item in _joysticks.Keys)
            {
                InitializeJoystickDevice(_directInput, item, handle);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    private void InitializeKeyboardDevice(IDirectInput8 directInput, Guid guid, IntPtr windowhandle)
    {
        IDirectInputDevice8 inputDevice = directInput.CreateDevice(guid);
        inputDevice.SetCooperativeLevel(windowhandle, CooperativeLevel.NonExclusive | CooperativeLevel.Foreground);

        // Play the values back to see that buffersize is working correctly
        inputDevice.Properties.BufferSize = 16;

        if (directInput.IsDeviceAttached(guid))
        {
            Result result = inputDevice.SetDataFormat<RawKeyboardState>();

            if (result.Success)
            {
                _keyboardDevices.Add(guid, inputDevice);
            }
        }
    }

    private void InitializeJoystickDevice(IDirectInput8 directInput, Guid guid, IntPtr windowhandle)
    {
        IDirectInputDevice8 inputDevice = directInput.CreateDevice(guid);
        inputDevice.SetCooperativeLevel(windowhandle, CooperativeLevel.NonExclusive | CooperativeLevel.Foreground);

        // Play the values back to see that buffersize is working correctly
        inputDevice.Properties.BufferSize = 16;

        if (directInput.IsDeviceAttached(guid))
        {
            Result result = inputDevice.SetDataFormat<RawJoystickState>();

            if (result.Success)
            {
                _joystickDevices.Add(guid, inputDevice);
            }
        }
    }

    public void GetKeyboardUpdates()
    {
        foreach (IDirectInputDevice8 keyboard in _keyboardDevices.Values)
        {
            Result result = keyboard.Poll();

            if (result.Failure)
            {
                result = keyboard.Acquire();

                if (result.Failure)
                    return;
            }

            try
            {
                KeyboardUpdate[] bufferedData = keyboard.GetBufferedKeyboardData();

                if (bufferedData.Length > 0)
                {
                    Console.WriteLine(bufferedData[0].ToString());
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }
        }
    }

    public void GetKJoystickUpdates()
    {
        foreach (var joystick in _joystickDevices)
        {
            Result result_ = joystick.Value.Poll();

            if (result_.Failure)
            {
                result_ = joystick.Value.Acquire();

                if (result_.Failure)
                    return;
            }

            try
            {
                JoystickUpdate[] bufferedData_ = joystick.Value.GetBufferedJoystickData();

                if (bufferedData_.Length > 0)
                {
                    Trace.WriteLine(bufferedData_[0].ToString());
                }
            }
            catch (Exception ex)
            {
                Trace.Write(ex.Message);
            }
        }
    }

    public static Dictionary<Guid, DeviceInstance> EnumerateAllAttachedKeyboardDevices(IDirectInput8 directInput)
    {
        Dictionary<Guid, DeviceInstance> result = new();

        foreach (DeviceInstance deviceInstance in directInput.GetDevices(DeviceClass.Keyboard, DeviceEnumerationFlags.AttachedOnly))
        {
            if (deviceInstance.Type == DeviceType.Keyboard)
            {
                result.Add(deviceInstance.InstanceGuid, deviceInstance);
            }
            else
            {
                Console.WriteLine(deviceInstance.ProductName + " does not match input type, ignored.");
            }
        }

        return result;
    }

    static public Dictionary<Guid, DeviceInstance> EnumerateAllAttachedGameDevices(IDirectInput8 directInput)
    {
        Dictionary<Guid, DeviceInstance> connectedDeviceList_ = new Dictionary<Guid, DeviceInstance>();

        foreach (DeviceInstance deviceInstance_ in directInput.GetDevices(DeviceClass.GameControl, DeviceEnumerationFlags.AttachedOnly))
        {
            if (deviceInstance_.Type == Vortice.DirectInput.DeviceType.Gamepad || deviceInstance_.Type == Vortice.DirectInput.DeviceType.Joystick)
            {
                connectedDeviceList_.Add(deviceInstance_.InstanceGuid, deviceInstance_);
            }
            else
            {
                Console.WriteLine(deviceInstance_.ProductName + " does not match input type, ignored.");
            }
        }

        return connectedDeviceList_;
    }
}
