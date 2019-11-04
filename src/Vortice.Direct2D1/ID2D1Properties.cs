// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using System.Numerics;
using SharpGen.Runtime;

namespace Vortice.Direct2D1
{
    public partial class ID2D1Properties
    {
        public bool Cached
        {
            set => SetValue((int)Property.Cached, value);
            get => GetBoolValue((int)Property.Cached);
        }

        public unsafe void SetValue(int index, bool value)
        {
            var intValue = value ? 1 : 0;
            SetValue(index, PropertyType.Bool, new IntPtr(&intValue), sizeof(int));
        }

        public unsafe void SetValue(int index, Guid value)
        {
            SetValue(index, PropertyType.Clsid, new IntPtr(&value), sizeof(Guid));
        }

        public unsafe void SetValue(int index, float value)
        {
            SetValue(index, PropertyType.Float, new IntPtr(&value), sizeof(float));
        }

        public unsafe void SetValue(int index, int value)
        {
            SetValue(index, PropertyType.Int32, new IntPtr(&value), sizeof(int));
        }

        public unsafe void SetValue(int index, Matrix3x2 value)
        {
            SetValue(index, PropertyType.Matrix3x2, new IntPtr(&value), sizeof(Matrix3x2));
        }

        public unsafe void SetValue(int index, Matrix4x3 value)
        {
            SetValue(index, PropertyType.Matrix4x3, new IntPtr(&value), sizeof(Matrix4x3));
        }

        public unsafe void SetValue(int index, Matrix4x4 value)
        {
            SetValue(index, PropertyType.Matrix4x4, new IntPtr(&value), sizeof(Matrix4x4));
        }

        public unsafe void SetValue(int index, RawMatrix5x4 value)
        {
            SetValue(index, PropertyType.Matrix5x4, new IntPtr(&value), sizeof(RawMatrix5x4));
        }

        public unsafe void SetValue(int index, string value)
        {
            fixed (char* chars = value)
            {
                SetValue(index, PropertyType.String, new IntPtr(chars), value.Length + 1);
            }
        }

        public unsafe void SetValue(int index, uint value)
        {
            SetValue(index, PropertyType.UInt32, new IntPtr(&value), sizeof(uint));
        }

        public unsafe void SetValue(int index, Vector2 value)
        {
            SetValue(index, PropertyType.Vector2, new IntPtr(&value), sizeof(Vector2));
        }

        public unsafe void SetValue(int index, Vector3 value)
        {
            SetValue(index, PropertyType.Vector3, new IntPtr(&value), sizeof(Vector3));
        }

        public unsafe void SetValue(int index, Vector4 value)
        {
            SetValue(index, PropertyType.Vector4, new IntPtr(&value), sizeof(Vector4));
        }

        public unsafe void SetValue<T>(int index, T value) where T : unmanaged, Enum
        {
            SetValue(index, PropertyType.Enum, new IntPtr(&value), sizeof(int));
        }

        public unsafe void SetValue(int index, ComObject comObject)
        {
            var ptr = comObject?.NativePointer ?? IntPtr.Zero;
            SetValue(index, PropertyType.IUnknown, new IntPtr(&ptr), sizeof(IntPtr));
        }

        public unsafe bool GetBoolValue(int index)
        {
            int value = default;
            GetValue(index, PropertyType.Bool, new IntPtr(&value), sizeof(int));
            return value != 0;
        }

        public unsafe Guid GetGuidValue(int index)
        {
            Guid value = default;
            GetValue(index, PropertyType.Clsid, new IntPtr(&value), sizeof(Guid));
            return value;
        }

        public unsafe float GetFloatValue(int index)
        {
            float value = default;
            GetValue(index, PropertyType.Float, new IntPtr(&value), sizeof(float));
            return value;
        }

        public unsafe int GetIntValue(int index)
        {
            int value = default;
            GetValue(index, PropertyType.Int32, new IntPtr(&value), sizeof(int));
            return value;
        }

        public unsafe Matrix3x2 GetMatrix3x2Value(int index)
        {
            Matrix3x2 value = default;
            GetValue(index, PropertyType.Matrix3x2, new IntPtr(&value), sizeof(Matrix3x2));
            return value;
        }

        public unsafe Matrix4x3 GetMatrix4x3Value(int index)
        {
            Matrix4x3 value = default;
            GetValue(index, PropertyType.Matrix4x3, new IntPtr(&value), sizeof(Matrix4x3));
            return value;
        }

        public unsafe Matrix4x4 GetMatrix4x4Value(int index)
        {
            Matrix4x4 value = default;
            GetValue(index, PropertyType.Matrix4x4, new IntPtr(&value), sizeof(Matrix4x4));
            return value;
        }

        public unsafe RawMatrix5x4 GetMatrix5x4Value(int index)
        {
            RawMatrix5x4 value = default;
            GetValue(index, PropertyType.Matrix5x4, new IntPtr(&value), sizeof(RawMatrix5x4));
            return value;
        }

        public unsafe string GetStringValue(int index)
        {
            var length = GetValueSize(index);
            char* chars = stackalloc char[length + 1];
            GetValue(index, PropertyType.String, new IntPtr(chars), length + 1);
            return new string(chars, 0, length);
        }

        public unsafe uint GetUintValue(int index)
        {
            uint value = default;
            GetValue(index, PropertyType.UInt32, new IntPtr(&value), sizeof(uint));
            return value;
        }

        public unsafe Vector2 GetVector2Value(int index)
        {
            Vector2 value = default;
            GetValue(index, PropertyType.Vector2, new IntPtr(&value), sizeof(Vector2));
            return value;
        }

        public unsafe Vector3 GetVector3Value(int index)
        {
            Vector3 value = default;
            GetValue(index, PropertyType.Vector3, new IntPtr(&value), sizeof(Vector3));
            return value;
        }

        public unsafe Vector4 GetVector4Value(int index)
        {
            Vector4 value = default;
            GetValue(index, PropertyType.Vector4, new IntPtr(&value), sizeof(Vector4));
            return value;
        }

        public unsafe T GetEnumValue<T>(int index) where T : unmanaged, Enum
        {
            T value = default;
            GetValue(index, PropertyType.Enum, new IntPtr(&value), sizeof(int));
            return value;
        }

        public unsafe T GetIUnknownValue<T>(int index) where T : ComObject
        {
            IntPtr value = default;
            GetValue(index, PropertyType.IUnknown, new IntPtr(&value), sizeof(IntPtr));
            return value == IntPtr.Zero ? null : As<T>(value);
        }
    }
}
