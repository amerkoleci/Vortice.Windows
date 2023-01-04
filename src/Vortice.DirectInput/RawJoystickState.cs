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

using System.Runtime.InteropServices;

namespace Vortice.DirectInput;

[StructLayout(LayoutKind.Sequential, Pack = 0)]
[DataFormat(DataFormatFlag.AbsoluteAxis)]
public unsafe partial struct RawJoystickState
{
    private const DeviceObjectTypeFlags TypeRelativeAxisOpt = DeviceObjectTypeFlags.RelativeAxis | DeviceObjectTypeFlags.AbsoluteAxis | DeviceObjectTypeFlags.AnyInstance | DeviceObjectTypeFlags.Optional;
    private const DeviceObjectTypeFlags TypePovOpt = DeviceObjectTypeFlags.PointOfViewController | DeviceObjectTypeFlags.AnyInstance | DeviceObjectTypeFlags.Optional;
    private const DeviceObjectTypeFlags TypeButtonOpt = DeviceObjectTypeFlags.PushButton | DeviceObjectTypeFlags.ToggleButton | DeviceObjectTypeFlags.AnyInstance | DeviceObjectTypeFlags.Optional;

    [DataObjectFormat(ObjectGuid.XAxisStr, TypeRelativeAxisOpt, ObjectDataFormatFlags.Position)]
    public int X;

    [DataObjectFormat(ObjectGuid.YAxisStr, TypeRelativeAxisOpt, ObjectDataFormatFlags.Position)]
    public int Y;

    [DataObjectFormat(ObjectGuid.ZAxisStr, TypeRelativeAxisOpt, ObjectDataFormatFlags.Position)]
    public int Z;

    [DataObjectFormat(ObjectGuid.RxAxisStr, TypeRelativeAxisOpt, ObjectDataFormatFlags.Position)]
    public int RotationX;

    [DataObjectFormat(ObjectGuid.RyAxisStr, TypeRelativeAxisOpt, ObjectDataFormatFlags.Position)]
    public int RotationY;

    [DataObjectFormat(ObjectGuid.RzAxisStr, TypeRelativeAxisOpt, ObjectDataFormatFlags.Position)]
    public int RotationZ;

    [DataObjectFormat(ObjectGuid.SliderStr, 2, TypeRelativeAxisOpt, ObjectDataFormatFlags.Position)]
    public fixed int Sliders[2];

    [DataObjectFormat(ObjectGuid.PovControllerStr, 4, TypePovOpt)]
    public fixed int PointOfViewControllers[4];

    [DataObjectFormat(128, TypeButtonOpt)]
    public fixed byte Buttons[128];

    [DataObjectFormat(ObjectGuid.XAxisStr, TypeRelativeAxisOpt, ObjectDataFormatFlags.Velocity)]
    public int VelocityX;
    [DataObjectFormat(ObjectGuid.YAxisStr, TypeRelativeAxisOpt, ObjectDataFormatFlags.Velocity)]
    public int VelocityY;
    [DataObjectFormat(ObjectGuid.ZAxisStr, TypeRelativeAxisOpt, ObjectDataFormatFlags.Velocity)]
    public int VelocityZ;
    [DataObjectFormat(ObjectGuid.RxAxisStr, TypeRelativeAxisOpt, ObjectDataFormatFlags.Velocity)]
    public int AngularVelocityX;
    [DataObjectFormat(ObjectGuid.RyAxisStr, TypeRelativeAxisOpt, ObjectDataFormatFlags.Velocity)]
    public int AngularVelocityY;
    [DataObjectFormat(ObjectGuid.RzAxisStr, TypeRelativeAxisOpt, ObjectDataFormatFlags.Velocity)]
    public int AngularVelocityZ;

    [DataObjectFormat(ObjectGuid.SliderStr, 2, TypeRelativeAxisOpt, ObjectDataFormatFlags.Velocity)]
    public fixed int VelocitySliders[2];

    [DataObjectFormat(ObjectGuid.XAxisStr, TypeRelativeAxisOpt, ObjectDataFormatFlags.Acceleration)]
    public int AccelerationX;

    [DataObjectFormat(ObjectGuid.YAxisStr, TypeRelativeAxisOpt, ObjectDataFormatFlags.Acceleration)]
    public int AccelerationY;

    [DataObjectFormat(ObjectGuid.ZAxisStr, TypeRelativeAxisOpt, ObjectDataFormatFlags.Acceleration)]
    public int AccelerationZ;

    [DataObjectFormat(ObjectGuid.RxAxisStr, TypeRelativeAxisOpt, ObjectDataFormatFlags.Acceleration)]
    public int AngularAccelerationX;

    [DataObjectFormat(ObjectGuid.RyAxisStr, TypeRelativeAxisOpt, ObjectDataFormatFlags.Acceleration)]
    public int AngularAccelerationY;

    [DataObjectFormat(ObjectGuid.RzAxisStr, TypeRelativeAxisOpt, ObjectDataFormatFlags.Acceleration)]
    public int AngularAccelerationZ;

    [DataObjectFormat(ObjectGuid.SliderStr, 2, TypeRelativeAxisOpt, ObjectDataFormatFlags.Acceleration)]
    public fixed int AccelerationSliders[2];

    [DataObjectFormat(ObjectGuid.XAxisStr, TypeRelativeAxisOpt, ObjectDataFormatFlags.Force)]
    public int ForceX;
    [DataObjectFormat(ObjectGuid.YAxisStr, TypeRelativeAxisOpt, ObjectDataFormatFlags.Force)]
    public int ForceY;
    [DataObjectFormat(ObjectGuid.ZAxisStr, TypeRelativeAxisOpt, ObjectDataFormatFlags.Force)]
    public int ForceZ;

    [DataObjectFormat(ObjectGuid.RxAxisStr, TypeRelativeAxisOpt, ObjectDataFormatFlags.Force)]
    public int TorqueX;
    [DataObjectFormat(ObjectGuid.RyAxisStr, TypeRelativeAxisOpt, ObjectDataFormatFlags.Force)]
    public int TorqueY;
    [DataObjectFormat(ObjectGuid.RzAxisStr, TypeRelativeAxisOpt, ObjectDataFormatFlags.Force)]
    public int TorqueZ;

    [DataObjectFormat(ObjectGuid.SliderStr, 2, TypeRelativeAxisOpt, ObjectDataFormatFlags.Force)]
    public fixed int ForceSliders[2];
}
