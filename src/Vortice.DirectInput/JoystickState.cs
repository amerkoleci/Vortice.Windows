// Copyright (c) 2010-2014 SharpDX - Alexandre Mutel
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.

namespace Vortice.DirectInput;

public class JoystickState : IDeviceState<RawJoystickState, JoystickUpdate>
{
    public JoystickState()
    {
        Sliders = new int[2];
        PointOfViewControllers = new int[4];
        Buttons = new bool[128];
        VelocitySliders = new int[2];
        AccelerationSliders = new int[2];
        ForceSliders = new int[2];
    }

    public int X { get; set; }
    public int Y { get; set; }
    public int Z { get; set; }
    public int RotationX { get; set; }
    public int RotationY { get; set; }
    public int RotationZ { get; set; }

    public int[] Sliders { get; internal set; }
    public int[] PointOfViewControllers { get; internal set; }
    public bool[] Buttons { get; internal set; }
    public int VelocityX { get; set; }
    public int VelocityY { get; set; }
    public int VelocityZ { get; set; }
    public int AngularVelocityX { get; set; }
    public int AngularVelocityY { get; set; }
    public int AngularVelocityZ { get; set; }
    public int[] VelocitySliders { get; internal set; }
    public int AccelerationX { get; set; }
    public int AccelerationY { get; set; }
    public int AccelerationZ { get; set; }
    public int AngularAccelerationX { get; set; }
    public int AngularAccelerationY { get; set; }
    public int AngularAccelerationZ { get; set; }
    public int[] AccelerationSliders { get; internal set; }
    public int ForceX { get; set; }
    public int ForceY { get; set; }
    public int ForceZ { get; set; }
    public int TorqueX { get; set; }
    public int TorqueY { get; set; }
    public int TorqueZ { get; set; }
    public int[] ForceSliders { get; internal set; }

    public void Update(JoystickUpdate update)
    {
        int value = update.Value;
        switch (update.Offset)
        {
            case JoystickOffset.X:
                X = value;
                break;
            case JoystickOffset.Y:
                Y = value;
                break;
            case JoystickOffset.Z:
                Z = value;
                break;
            case JoystickOffset.RotationX:
                RotationX = value;
                break;
            case JoystickOffset.RotationY:
                RotationY = value;
                break;
            case JoystickOffset.RotationZ:
                RotationZ = value;
                break;
            case JoystickOffset.Sliders0:
                Sliders[0] = value;
                break;
            case JoystickOffset.Sliders1:
                Sliders[1] = value;
                break;
            case JoystickOffset.PointOfViewControllers0:
                PointOfViewControllers[0] = value;
                break;
            case JoystickOffset.PointOfViewControllers1:
                PointOfViewControllers[1] = value;
                break;
            case JoystickOffset.PointOfViewControllers2:
                PointOfViewControllers[2] = value;
                break;
            case JoystickOffset.PointOfViewControllers3:
                PointOfViewControllers[3] = value;
                break;
            case JoystickOffset.VelocityX:
                VelocityX = value;
                break;
            case JoystickOffset.VelocityY:
                VelocityY = value;
                break;
            case JoystickOffset.VelocityZ:
                VelocityZ = value;
                break;
            case JoystickOffset.AngularVelocityX:
                AngularVelocityX = value;
                break;
            case JoystickOffset.AngularVelocityY:
                AngularVelocityY = value;
                break;
            case JoystickOffset.AngularVelocityZ:
                AngularVelocityZ = value;
                break;
            case JoystickOffset.VelocitySliders0:
                VelocitySliders[0] = value;
                break;
            case JoystickOffset.VelocitySliders1:
                VelocitySliders[1] = value;
                break;
            case JoystickOffset.AccelerationX:
                AccelerationX = value;
                break;
            case JoystickOffset.AccelerationY:
                AccelerationY = value;
                break;
            case JoystickOffset.AccelerationZ:
                AccelerationZ = value;
                break;
            case JoystickOffset.AngularAccelerationX:
                AngularAccelerationX = value;
                break;
            case JoystickOffset.AngularAccelerationY:
                AngularAccelerationY = value;
                break;
            case JoystickOffset.AngularAccelerationZ:
                AngularAccelerationZ = value;
                break;
            case JoystickOffset.AccelerationSliders0:
                AccelerationSliders[0] = value;
                break;
            case JoystickOffset.AccelerationSliders1:
                AccelerationSliders[1] = value;
                break;
            case JoystickOffset.ForceX:
                ForceX = value;
                break;
            case JoystickOffset.ForceY:
                ForceY = value;
                break;
            case JoystickOffset.ForceZ:
                ForceZ = value;
                break;
            case JoystickOffset.TorqueX:
                TorqueX = value;
                break;
            case JoystickOffset.TorqueY:
                TorqueY = value;
                break;
            case JoystickOffset.TorqueZ:
                TorqueZ = value;
                break;
            case JoystickOffset.ForceSliders0:
                ForceSliders[0] = value;
                break;
            case JoystickOffset.ForceSliders1:
                ForceSliders[1] = value;
                break;
            default:
                int buttonIndex = update.Offset - JoystickOffset.Buttons0;
                if (buttonIndex >= 0 && buttonIndex < 128)
                    Buttons[buttonIndex] = (value & 0x80) != 0;
                break;
        }
    }

    public void MarshalFrom(ref RawJoystickState value)
    {
        unsafe
        {
            X = value.X;
            Y = value.Y;
            Z = value.Z;
            RotationX = value.RotationX;
            RotationY = value.RotationY;
            RotationZ = value.RotationZ;
            fixed (int* __from = value.Sliders)
            {
                Sliders[0] = __from[0]; Sliders[1] = __from[1];
            }

            fixed (int* __from = value.PointOfViewControllers)
            {
                PointOfViewControllers[0] = __from[0];
                PointOfViewControllers[1] = __from[1];
                PointOfViewControllers[2] = __from[2];
                PointOfViewControllers[3] = __from[3];
            }

            fixed (void* __from = value.Buttons)
            {
                for (int i = 0; i < 128; i++)
                    Buttons[i] = (((byte*)__from)[i] & 0x80) != 0;
            }

            VelocityX = value.VelocityX;
            VelocityY = value.VelocityY;
            VelocityZ = value.VelocityZ;
            AngularVelocityX = value.AngularVelocityX;
            AngularVelocityY = value.AngularVelocityY;
            AngularVelocityZ = value.AngularVelocityZ;

            fixed (int* __from = value.VelocitySliders)
            {
                VelocitySliders[0] = __from[0]; VelocitySliders[1] = __from[1];
            }

            AccelerationX = value.AccelerationX;
            AccelerationY = value.AccelerationY;
            AccelerationZ = value.AccelerationZ;
            AngularAccelerationX = value.AngularAccelerationX;
            AngularAccelerationY = value.AngularAccelerationY;
            AngularAccelerationZ = value.AngularAccelerationZ;
            fixed (int* __from = value.AccelerationSliders)
            {
                AccelerationSliders[0] = __from[0]; AccelerationSliders[1] = __from[1];
            }
            ForceX = value.ForceX;
            ForceY = value.ForceY;
            ForceZ = value.ForceZ;
            TorqueX = value.TorqueX;
            TorqueY = value.TorqueY;
            TorqueZ = value.TorqueZ;

            fixed (int* __from = value.ForceSliders)
            {
                ForceSliders[0] = __from[0]; ForceSliders[1] = __from[1];
            }
        }
    }

    public override string ToString()
    {
        return string.Format(System.Globalization.CultureInfo.InvariantCulture,
            "X: {0}, Y: {1}, Z: {2}, RotationX: {3}, RotationY: {4}, RotationZ: {5}, Sliders: {6}, PointOfViewControllers: {7}, Buttons: {8}, VelocityX: {9}, VelocityY: {10}, VelocityZ: {11}, AngularVelocityX: {12}, AngularVelocityY: {13}, AngularVelocityZ: {14}, VelocitySliders: {15}, AccelerationX: {16}, AccelerationY: {17}, AccelerationZ: {18}, AngularAccelerationX: {19}, AngularAccelerationY: {20}, AngularAccelerationZ: {21}, AccelerationSliders: {22}, ForceX: {23}, ForceY: {24}, ForceZ: {25}, TorqueX: {26}, TorqueY: {27}, TorqueZ: {28}, ForceSliders: {29}",
            X, Y, Z, RotationX, RotationY, RotationZ,
            string.Join(";", Sliders),
            string.Join(";", PointOfViewControllers),
            string.Join(";", Buttons), VelocityX, VelocityY, VelocityZ, AngularVelocityX, AngularVelocityY, AngularVelocityZ, string.Join(";", VelocitySliders), AccelerationX, AccelerationY, AccelerationZ, AngularAccelerationX, AngularAccelerationY, AngularAccelerationZ, string.Join(";", AccelerationSliders),
            ForceX, ForceY, ForceZ,
            TorqueX, TorqueY, TorqueZ,
            string.Join(";", ForceSliders)
            );
    }
}
