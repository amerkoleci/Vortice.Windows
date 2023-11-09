// Copyright (c) Amer Koleci and contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using System.Drawing;

namespace Vortice.Direct2D1;

public unsafe partial class ID2D1Properties
{
    public bool Cached
    {
        set => SetValue((int)Property.Cached, value);
        get => GetBoolValue((int)Property.Cached);
    }

    public void SetValue(int index, bool value)
    {
        int intValue = value ? 1 : 0;
        SetValue(index, PropertyType.Bool, &intValue, sizeof(RawBool));
    }

    public void SetValue(int index, Guid value)
    {
        SetValue(index, PropertyType.Clsid, &value, sizeof(Guid));
    }

    public void SetValue(int index, float value)
    {
        SetValue(index, PropertyType.Float, &value, sizeof(float));
    }

    public void SetValue(int index, int value)
    {
        SetValue(index, PropertyType.Int32, &value, sizeof(int));
    }

    public void SetValue(int index, Matrix3x2 value)
    {
        SetValue(index, PropertyType.Matrix3x2, &value, sizeof(Matrix3x2));
    }

    public void SetValue(int index, Matrix4x3 value)
    {
        SetValue(index, PropertyType.Matrix4x3, &value, sizeof(Matrix4x3));
    }

    public void SetValue(int index, Matrix4x4 value)
    {
        SetValue(index, PropertyType.Matrix4x4, &value, sizeof(Matrix4x4));
    }

    public void SetValue(int index, Matrix5x4 value)
    {
        SetValue(index, PropertyType.Matrix5x4, &value, sizeof(Matrix5x4));
    }

    public void SetValue(int index, string value)
    {
        fixed (char* chars = value)
        {
            SetValue(index, PropertyType.String, chars, value.Length + 1);
        }
    }

    public void SetValue(int index, uint value)
    {
        SetValue(index, PropertyType.UInt32, &value, sizeof(uint));
    }

    public void SetValue(int index, Vector2 value)
    {
        SetValue(index, PropertyType.Vector2, &value, sizeof(Vector2));
    }

    public void SetValue(int index, Vector3 value)
    {
        SetValue(index, PropertyType.Vector3, &value, sizeof(Vector3));
    }

    public void SetValue(int index, Vector4 value)
    {
        SetValue(index, PropertyType.Vector4, &value, sizeof(Vector4));
    }

    public void SetValue(int index, RectangleF value)
    {
        Vector4 vector = new(value.X, value.Y, value.Width, value.Height);
        SetValue(index, PropertyType.Vector4, &vector, sizeof(Vector4));
    }

    public void SetValue<T>(int index, T value) where T : unmanaged, Enum
    {
        SetValue(index, PropertyType.Enum, &value, sizeof(int));
    }

    public void SetValue(int index, ComObject? comObject)
    {
        IntPtr ptr = comObject?.NativePointer ?? IntPtr.Zero;
        SetValue(index, PropertyType.IUnknown, &ptr, sizeof(IntPtr));
    }

    public void SetValue(int index, ID2D1ColorContext? colorContext)
    {
        IntPtr ptr = colorContext?.NativePointer ?? IntPtr.Zero;
        SetValue(index, PropertyType.ColorContext, &ptr, sizeof(IntPtr));
    }

    public bool GetBoolValue(int index)
    {
        int value = default;
        GetValue(index, PropertyType.Bool, &value, sizeof(int));
        return value != 0;
    }

    public Guid GetGuidValue(int index)
    {
        Guid value = default;
        GetValue(index, PropertyType.Clsid, &value, sizeof(Guid));
        return value;
    }

    public float GetFloatValue(int index)
    {
        float value = default;
        GetValue(index, PropertyType.Float, &value, sizeof(float));
        return value;
    }

    public int GetIntValue(int index)
    {
        int value = default;
        GetValue(index, PropertyType.Int32, &value, sizeof(int));
        return value;
    }

    public Matrix3x2 GetMatrix3x2Value(int index)
    {
        Matrix3x2 value = default;
        GetValue(index, PropertyType.Matrix3x2, &value, sizeof(Matrix3x2));
        return value;
    }

    public Matrix4x3 GetMatrix4x3Value(int index)
    {
        Matrix4x3 value = default;
        GetValue(index, PropertyType.Matrix4x3, &value, sizeof(Matrix4x3));
        return value;
    }

    public Matrix4x4 GetMatrix4x4Value(int index)
    {
        Matrix4x4 value = default;
        GetValue(index, PropertyType.Matrix4x4, &value, sizeof(Matrix4x4));
        return value;
    }

    public Matrix5x4 GetMatrix5x4Value(int index)
    {
        Matrix5x4 value = default;
        GetValue(index, PropertyType.Matrix5x4, &value, sizeof(Matrix5x4));
        return value;
    }

    public string GetStringValue(int index)
    {
        var length = GetValueSize(index);
        char* chars = stackalloc char[length + 1];
        GetValue(index, PropertyType.String, chars, length + 1);
        return new string(chars, 0, length);
    }

    public uint GetUIntValue(int index)
    {
        uint value = default;
        GetValue(index, PropertyType.UInt32, &value, sizeof(uint));
        return value;
    }

    public Vector2 GetVector2Value(int index)
    {
        Vector2 value = default;
        GetValue(index, PropertyType.Vector2, &value, sizeof(Vector2));
        return value;
    }

    public unsafe Vector3 GetVector3Value(int index)
    {
        Vector3 value = default;
        GetValue(index, PropertyType.Vector3, &value, sizeof(Vector3));
        return value;
    }

    public unsafe Vector4 GetVector4Value(int index)
    {
        Vector4 value = default;
        GetValue(index, PropertyType.Vector4, &value, sizeof(Vector4));
        return value;
    }

    public unsafe T GetEnumValue<T>(int index) where T : unmanaged, Enum
    {
        T value = default;
        GetValue(index, PropertyType.Enum, &value, sizeof(int));
        return value;
    }

    public unsafe T? GetIUnknownValue<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] T>(int index) where T : ComObject
    {
        IntPtr value = default;
        GetValue(index, PropertyType.IUnknown, &value, sizeof(IntPtr));
        return value == IntPtr.Zero ? default : As<T>(value);
    }
    public ID2D1ColorContext? GetColorContextValue(int index)
    {
        IntPtr value = default;
        GetValue(index, PropertyType.ColorContext, &value, sizeof(IntPtr));
        return value == IntPtr.Zero ? default : new ID2D1ColorContext(value);
    }
}
