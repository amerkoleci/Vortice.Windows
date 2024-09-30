// Copyright (c) Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using System.Drawing;

namespace Vortice.Direct2D1;

public unsafe partial class ID2D1Properties
{
    public bool Cached
    {
        get => GetBoolValue((uint)Property.Cached);
        set => SetValue((uint)Property.Cached, value);
    }

    public void SetValue(uint index, bool value)
    {
        int intValue = value ? 1 : 0;
        SetValue(index, PropertyType.Bool, &intValue, (uint)sizeof(RawBool));
    }

    public void SetValue(uint index, Guid value)
    {
        SetValue(index, PropertyType.Clsid, &value, (uint)sizeof(Guid));
    }

    public void SetValue(uint index, float value)
    {
        SetValue(index, PropertyType.Float, &value, sizeof(float));
    }

    public void SetValue(uint index, int value)
    {
        SetValue(index, PropertyType.Int32, &value, sizeof(int));
    }

    public void SetValue(uint index, Matrix3x2 value)
    {
        SetValue(index, PropertyType.Matrix3x2, &value, (uint)sizeof(Matrix3x2));
    }

    public void SetValue(uint index, Matrix4x3 value)
    {
        SetValue(index, PropertyType.Matrix4x3, &value, (uint)sizeof(Matrix4x3));
    }

    public void SetValue(uint index, Matrix4x4 value)
    {
        SetValue(index, PropertyType.Matrix4x4, &value, (uint)sizeof(Matrix4x4));
    }

    public void SetValue(uint index, Matrix5x4 value)
    {
        SetValue(index, PropertyType.Matrix5x4, &value, (uint)sizeof(Matrix5x4));
    }

    public void SetValue(uint index, string value)
    {
        fixed (char* chars = value)
        {
            SetValue(index, PropertyType.String, chars, (uint)value.Length + 1);
        }
    }

    public void SetValue(uint index, uint value)
    {
        SetValue(index, PropertyType.UInt32, &value, (uint)sizeof(uint));
    }

    public void SetValue(uint index, Vector2 value)
    {
        SetValue(index, PropertyType.Vector2, &value, (uint)sizeof(Vector2));
    }

    public void SetValue(uint index, Vector3 value)
    {
        SetValue(index, PropertyType.Vector3, &value, (uint)sizeof(Vector3));
    }

    public void SetValue(uint index, Vector4 value)
    {
        SetValue(index, PropertyType.Vector4, &value, (uint)sizeof(Vector4));
    }

    public void SetValue(uint index, RectangleF value)
    {
        Vector4 vector = new(value.X, value.Y, value.Width, value.Height);
        SetValue(index, PropertyType.Vector4, &vector, (uint)sizeof(Vector4));
    }

    public void SetValue<T>(uint index, T value) where T : unmanaged, Enum
    {
        SetValue(index, PropertyType.Enum, &value, (uint)sizeof(int));
    }

    public void SetValue(uint index, ComObject? comObject)
    {
        IntPtr ptr = comObject?.NativePointer ?? IntPtr.Zero;
        SetValue(index, PropertyType.IUnknown, &ptr, (uint)sizeof(IntPtr));
    }

    public void SetValue(uint index, ID2D1ColorContext? colorContext)
    {
        IntPtr ptr = colorContext?.NativePointer ?? IntPtr.Zero;
        SetValue(index, PropertyType.ColorContext, &ptr, (uint)sizeof(IntPtr));
    }

    public bool GetBoolValue(uint index)
    {
        int value = default;
        GetValue(index, PropertyType.Bool, &value, (uint)sizeof(int));
        return value != 0;
    }

    public Guid GetGuidValue(uint index)
    {
        Guid value = default;
        GetValue(index, PropertyType.Clsid, &value, (uint)sizeof(Guid));
        return value;
    }

    public float GetFloatValue(uint index)
    {
        float value = default;
        GetValue(index, PropertyType.Float, &value, (uint)sizeof(float));
        return value;
    }

    public int GetIntValue(uint index)
    {
        int value = default;
        GetValue(index, PropertyType.Int32, &value, (uint)sizeof(int));
        return value;
    }

    public Matrix3x2 GetMatrix3x2Value(uint index)
    {
        Matrix3x2 value = default;
        GetValue(index, PropertyType.Matrix3x2, &value, (uint)sizeof(Matrix3x2));
        return value;
    }

    public Matrix4x3 GetMatrix4x3Value(uint index)
    {
        Matrix4x3 value = default;
        GetValue(index, PropertyType.Matrix4x3, &value, (uint)sizeof(Matrix4x3));
        return value;
    }

    public Matrix4x4 GetMatrix4x4Value(uint index)
    {
        Matrix4x4 value = default;
        GetValue(index, PropertyType.Matrix4x4, &value, (uint)sizeof(Matrix4x4));
        return value;
    }

    public Matrix5x4 GetMatrix5x4Value(uint index)
    {
        Matrix5x4 value = default;
        GetValue(index, PropertyType.Matrix5x4, &value, (uint)sizeof(Matrix5x4));
        return value;
    }

    public string GetStringValue(uint index)
    {
        uint length = GetValueSize(index);
        char* chars = stackalloc char[(int)length + 1];
        GetValue(index, PropertyType.String, chars, length + 1);
        return new string(chars, 0, (int)length);
    }

    public uint GetUIntValue(uint index)
    {
        uint value = default;
        GetValue(index, PropertyType.UInt32, &value, sizeof(uint));
        return value;
    }

    public Vector2 GetVector2Value(uint index)
    {
        Vector2 value = default;
        GetValue(index, PropertyType.Vector2, &value, (uint)sizeof(Vector2));
        return value;
    }

    public unsafe Vector3 GetVector3Value(uint index)
    {
        Vector3 value = default;
        GetValue(index, PropertyType.Vector3, &value, (uint)sizeof(Vector3));
        return value;
    }

    public unsafe Vector4 GetVector4Value(uint index)
    {
        Vector4 value = default;
        GetValue(index, PropertyType.Vector4, &value, (uint)sizeof(Vector4));
        return value;
    }

    public unsafe T GetEnumValue<T>(uint index) where T : unmanaged, Enum
    {
        T value = default;
        GetValue(index, PropertyType.Enum, &value, (uint)sizeof(int));
        return value;
    }

    public unsafe T? GetIUnknownValue<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] T>(uint index)
        where T : ComObject
    {
        IntPtr value = default;
        GetValue(index, PropertyType.IUnknown, &value, (uint)sizeof(IntPtr));
        return value == IntPtr.Zero ? default : As<T>(value);
    }
    public ID2D1ColorContext? GetColorContextValue(uint index)
    {
        IntPtr value = default;
        GetValue(index, PropertyType.ColorContext, &value, (uint)sizeof(nint));
        return value == IntPtr.Zero ? default : new ID2D1ColorContext(value);
    }
}
