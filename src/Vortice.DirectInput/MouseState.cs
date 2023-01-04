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

public class MouseState : IDeviceState<RawMouseState, MouseUpdate>
{
    public MouseState()
    {
        Buttons = new bool[8];
    }

    public int X { get; set; }

    public int Y { get; set; }

    public int Z { get; set; }

    public bool[] Buttons { get; private set; }

    public void Update(MouseUpdate update)
    {
        int value = update.Value;
        switch (update.Offset)
        {
            case MouseOffset.X:
                X = value;
                break;
            case MouseOffset.Y:
                Y = value;
                break;
            case MouseOffset.Z:
                Z = value;
                break;
            default:
                int buttonIndex = update.Offset - MouseOffset.Buttons0;
                if (buttonIndex >= 0 && buttonIndex < 8)
                    Buttons[buttonIndex] = (value & 0x80) != 0;
                break;
        }
    }

    public void MarshalFrom(ref RawMouseState value)
    {
        unsafe
        {
            X = value.X;
            Y = value.Y;
            Z = value.Z;

            // Copy buttons states
            fixed (void* __from = &value.Buttons0)
            {
                for (int i = 0; i < 8; i++)
                {
                    Buttons[i] = (((byte*)__from)[i] & 0x80) != 0;
                }
            }
        }
    }

    public override string ToString()
    {
        return string.Format(System.Globalization.CultureInfo.InvariantCulture, "X: {0}, Y: {1}, Z: {2}, Buttons: {3}", X, Y, Z, string.Join(";", Buttons));
    }
}
