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
    class DirectInputDevice
    {
        IDirectInput8? DirectInput;

        Dictionary<Guid, IDirectInputDevice8> _keyboardDevices;


        Dictionary<Guid, DeviceInstance> _keyboards;
        Dictionary<Guid, DeviceInstance> _joysticks;

        public DirectInputDevice()
        {
            _keyboardDevices = new Dictionary<Guid, IDirectInputDevice8>();
        }

        public void Initialise(IntPtr handle)
        {
            try
            {
                DirectInput = DInput.DirectInput8Create();

                _keyboards = EnumerateAllAttachedKeyboardDevices(DirectInput);

                foreach (var item_ in _keyboards)
                {
                    InitialiseDevice(DirectInput, item_.Key, handle);
                }

                _joysticks = EnumerateAllAttachedGameDevices(DirectInput);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }


        void InitialiseDevice(IDirectInput8 directInput, Guid guid, IntPtr windowhandle)
        {
            IDirectInputDevice8 inputDevice_ = directInput.CreateDevice(guid);

            Result result_ = inputDevice_.SetCooperativeLevel(windowhandle, CooperativeLevel.Exclusive | CooperativeLevel.Foreground);

            if (directInput.IsDeviceAttached(guid))
            {
                if (result_.Success)
                {
                    result_ = inputDevice_.SetDataFormat<RawKeyboardState>();

                    if (result_.Success)
                    {
                        result_ = inputDevice_.Acquire();

                        if (result_.Success)
                        {
                            KeyboardState data = inputDevice_.GetCurrentKeyboardState();

                            _keyboardDevices.Add(guid, inputDevice_);
                        }
                    }
                }
            }
        }


        //==========================================================================================================================//

        public void GetKeyboardUpdates()
        {
            foreach (var keyboard in _keyboardDevices)
            {
                Result result_ = keyboard.Value.Poll();

                if (result_.Failure)
                {
                    keyboard.Value.Acquire();
                }

                try
                {
                    KeyboardUpdate[] bufferedData_ = keyboard.Value.GetBufferedKeyboardData();
                }
                catch (Exception ex)
                {
                    Trace.Write(ex.Message);
                }
            }
        }



        //==========================================================================================================================//

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
