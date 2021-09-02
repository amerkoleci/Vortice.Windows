// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpGen.Runtime;
using Vortice.DirectInput;

namespace HelloDirectInput
{
    class DIInputDevice
    {
        protected IDirectInput8? DirectInput;

        protected Dictionary<Guid, DeviceInstance>? _keyboards;
        protected Dictionary<Guid, DeviceInstance>? _joysticks;

        protected Dictionary<Guid, IDirectInputDevice8> _keyboardDevices;
        protected Dictionary<Guid, IDirectInputDevice8> _joystickDevices;

        public DIInputDevice()
        {
            _keyboardDevices = new Dictionary<Guid, IDirectInputDevice8>();
            _joystickDevices = new Dictionary<Guid, IDirectInputDevice8>();
        }

        public void Initialise(IntPtr handle)
        {
            try
            {
                DirectInput = DInput.DirectInput8Create();

                _keyboards = EnumerateAllAttachedKeyboardDevices(DirectInput);

                foreach (var item_ in _keyboards)
                {
                    InitialiseKeyboardDevice(DirectInput, item_.Key, handle);
                }

                _joysticks = EnumerateAllAttachedGameDevices(DirectInput);

                foreach (var item_ in _joysticks)
                {
                    InitialiseJoystickDevice(DirectInput, item_.Key, handle);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        void InitialiseKeyboardDevice(IDirectInput8 directInput, Guid guid, IntPtr windowhandle)
        {
            IDirectInputDevice8 inputDevice_ = directInput.CreateDevice(guid);

            Result result_ = inputDevice_.SetCooperativeLevel(windowhandle, CooperativeLevel.NonExclusive | CooperativeLevel.Foreground);

            // play the values back to see that buffersize is working correctly
            inputDevice_.Properties.BufferSize = 16;

            int size = inputDevice_.Properties.BufferSize;

            if (directInput.IsDeviceAttached(guid))
            {

                result_ = inputDevice_.SetDataFormat<RawKeyboardState>();

                if (result_.Success)
                {

                    _keyboardDevices.Add(guid, inputDevice_);
                }
            }
        }

        void InitialiseJoystickDevice(IDirectInput8 directInput, Guid guid, IntPtr windowhandle)
        {
            IDirectInputDevice8 inputDevice_ = directInput.CreateDevice(guid);

            Result result_ = inputDevice_.SetCooperativeLevel(windowhandle, CooperativeLevel.NonExclusive | CooperativeLevel.Foreground);

            // play the values back to see that buffersize is working correctly
            inputDevice_.Properties.BufferSize = 16;

            int size = inputDevice_.Properties.BufferSize;

            if (directInput.IsDeviceAttached(guid))
            {
                result_ = inputDevice_.SetDataFormat<RawJoystickState>();

                if (result_.Success)
                {
                    _joystickDevices.Add(guid, inputDevice_);
                }
            }
        }

        public void GetKeyboardUpdates()
        {
            foreach (var keyboard in _keyboardDevices)
            {
                Result result_ = keyboard.Value.Poll();

                if (result_.Failure)
                {
                    result_ = keyboard.Value.Acquire();

                    if (result_.Failure)
                        return;
                }

                try
                {
                    KeyboardUpdate[] bufferedData_ = keyboard.Value.GetBufferedKeyboardData();

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

        static public Dictionary<Guid, DeviceInstance> EnumerateAllAttachedKeyboardDevices(IDirectInput8 directInput)
        {
            Dictionary<Guid, DeviceInstance> connectedDeviceList_ = new Dictionary<Guid, DeviceInstance>();

            foreach (DeviceInstance deviceInstance_ in directInput.GetDevices(DeviceClass.Keyboard, DeviceEnumerationFlags.AttachedOnly))
            {
                if (deviceInstance_.Type == Vortice.DirectInput.DeviceType.Keyboard)
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
}
