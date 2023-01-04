// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System.Runtime.InteropServices;

namespace Vortice.DirectInput;

[StructLayout(LayoutKind.Sequential, Size = 4)]
public partial struct DeviceObjectId
{
    private int _rawType;

    private const int InstanceNumberMax = 0xFFFF - 1;
    private const int AnyInstanceMask = 0x00FFFF00;

    public DeviceObjectId(DeviceObjectTypeFlags typeFlags, int instanceNumber) : this()
    {
        // Clear anyInstance flags and use instanceNumber
        _rawType = ((int)typeFlags & ~AnyInstanceMask) | ((instanceNumber < 0 | instanceNumber > InstanceNumberMax) ? 0 : ((instanceNumber & 0xFFFF) << 8));
    }

    public DeviceObjectTypeFlags Flags
    {
        get
        {
            return (DeviceObjectTypeFlags)(_rawType & ~AnyInstanceMask);
        }
    }

    public int InstanceNumber
    {
        get { return (_rawType >> 8) & 0xFFFF; }
    }

    public static explicit operator int(DeviceObjectId type)
    {
        return type._rawType;
    }

    public bool Equals(DeviceObjectId other)
    {
        return other._rawType == _rawType;
    }

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (obj.GetType() != typeof(DeviceObjectId)) return false;
        return Equals((DeviceObjectId)obj);
    }

    public override int GetHashCode()
    {
        return _rawType;
    }

    public override string ToString()
    {
        return string.Format(System.Globalization.CultureInfo.InvariantCulture, "Flags: {0} InstanceNumber: {1} RawId: 0x{2:X8}", Flags, InstanceNumber, _rawType);
    }
}
