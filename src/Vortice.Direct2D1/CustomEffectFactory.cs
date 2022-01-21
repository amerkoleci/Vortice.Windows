// Copyright © Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using System.Reflection;
using System.Numerics;
using System.Drawing;
using Vortice.Mathematics;

namespace Vortice.Direct2D1;

internal class CustomEffectFactory
{
    public FunctionCallback Callback { protected set; get; }

    private readonly Type _effectType;
    private Func<ID2D1EffectImpl> _createID2D1EffectImplFunc;
    private readonly CreateCustomEffectDelegate _createEffect;
    private readonly PropertyNativeBase[] _propertyNatives;

    public CustomEffectFactory(Type effectType, Func<ID2D1EffectImpl> createID2D1EffectImplFunc)
    {
        _effectType = effectType;
        _createID2D1EffectImplFunc = createID2D1EffectImplFunc;

        _createEffect = new CreateCustomEffectDelegate(CreateCustomEffectImpl);
        Callback = new FunctionCallback(Marshal.GetFunctionPointerForDelegate(_createEffect));

        _propertyNatives = GetPropertyNatives().ToArray();
    }

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    private delegate int CreateCustomEffectDelegate(out IntPtr nativeCustomEffectPtr);

    private int CreateCustomEffectImpl(out IntPtr nativeCustomEffectPtr)
    {
        nativeCustomEffectPtr = IntPtr.Zero;
        try
        {
            var customEffect = _createID2D1EffectImplFunc();
            nativeCustomEffectPtr = MarshallingHelpers.ToCallbackPtr<ID2D1EffectImpl>(customEffect);
        }
        catch (SharpGenException ex)
        {
            return ex.HResult;
        }

        return Result.Ok.Code;
    }

    private IEnumerable<PropertyNativeBase?> GetPropertyNatives()
    {
        return _effectType.GetTypeInfo()
            .DeclaredProperties
            .Select(x => (property: x, attribute: x.GetCustomAttribute<CustomEffectPropertyAttribute>()))
            .Where(x => x.attribute != null)
            .OrderBy(x => x.attribute.Order)
            .Select(x => PropertyNative<int>.Create(x.property));
    }

    public string GetXML()
    {
        var attribute = _effectType.GetCustomAttribute<CustomEffectAttribute>();
        string xml =
            $"<?xml version='1.0'?>" +
            $"<Effect>" +
            $"<Property name='DisplayName' type='string' value='{attribute?.DisplayName ?? _effectType.Name}'/>" +
            $"<Property name='Author' type='string' value='{attribute?.Author ?? string.Empty}'/>" +
            $"<Property name='Category' type='string' value='{attribute?.Category ?? string.Empty}'/>" +
            $"<Property name='Description' type='string' value='{attribute?.Description ?? string.Empty}'/>";
        if (attribute?.Inputs != null && attribute.Inputs.Length > 0)
        {
            xml += "<Inputs>";
            foreach (var input in attribute.Inputs)
                xml += $"<Input name='{input}'/>";
            xml += "</Inputs>";
        }
        else
        {
            xml += "<Inputs/>";
        }

        foreach (var property in _propertyNatives)
        {
            xml += $"<Property name='{property.PropertyInfo.Name}' type='{property.PropertyType.ToString("G").ToLower()}'>";
            xml += $"<Property name='DisplayName' type='string' value='{property.PropertyInfo.Name}'/>";
            xml += $"</Property>";
        }
        xml += "</Effect>";
        return xml;

    }
    public PropertyBinding[] GetBindings()
    {
        var bindings = new PropertyBinding[_propertyNatives.Length];
        for (var i = 0; i < _propertyNatives.Length; i++)
        {
            bindings[i] = new PropertyBinding()
            {
                GetFunction = _propertyNatives[i].GetterPointer,
                SetFunction = _propertyNatives[i].SetterPointer,
                PropertyName = _propertyNatives[i].PropertyInfo.Name
            };
        }

        return bindings;
    }

    private abstract class PropertyNativeBase
    {
        public PropertyType PropertyType { get; }
        public PropertyInfo PropertyInfo { get; }

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        protected delegate int SetterDelegate(IntPtr thisPtr, IntPtr dataPtr, int dataSize);
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        protected delegate int GetterDelegate(IntPtr thisPtr, IntPtr dataPtr, int dataSize, out int actualSize);
        protected SetterDelegate? setterDelegate;
        protected GetterDelegate? getterDelegate;
        public IntPtr GetterPointer;
        public IntPtr SetterPointer;

        public PropertyNativeBase(PropertyInfo propertyInfo, PropertyType propertyType)
        {
            PropertyType = propertyType;
            PropertyInfo = propertyInfo;
        }
    }

    private class PropertyNative<U> : PropertyNativeBase where U : unmanaged
    {
        private PropertyNative(PropertyInfo propertyInfo, PropertyType propertyType) : base(propertyInfo, propertyType)
        {
            if (propertyInfo.CanWrite)
            {
                setterDelegate = new SetterDelegate(SetterImpl);
                SetterPointer = Marshal.GetFunctionPointerForDelegate(setterDelegate);
            }
            if (propertyInfo.CanRead)
            {
                getterDelegate = new GetterDelegate(GetterImpl);
                GetterPointer = Marshal.GetFunctionPointerForDelegate(getterDelegate);
            }
        }

        public static PropertyNativeBase? Create(PropertyInfo propertyInfo)
        {
            var type = propertyInfo.PropertyType;
            if (type == typeof(int))
                return new PropertyNative<int>(propertyInfo, PropertyType.Int32);
            else if (type == typeof(uint))
                return new PropertyNative<uint>(propertyInfo, PropertyType.UInt32);
            else if (type == typeof(float))
                return new PropertyNative<float>(propertyInfo, PropertyType.Float);
            else if (type == typeof(bool))
                return new PropertyNative<bool>(propertyInfo, PropertyType.Bool);
            else if (type == typeof(Vector2))
                return new PropertyNative<Vector2>(propertyInfo, PropertyType.Vector2);
            else if (type == typeof(Vector3))
                return new PropertyNative<Vector3>(propertyInfo, PropertyType.Vector3);
            else if (type == typeof(Color3))
                return new PropertyNative<Color3>(propertyInfo, PropertyType.Vector3);
            else if (type == typeof(Vector4))
                return new PropertyNative<Vector4>(propertyInfo, PropertyType.Vector4);
            else if (type == typeof(RawRectF))
                return new PropertyNative<RawRectF>(propertyInfo, PropertyType.Vector4);
            else if (type == typeof(RectangleF))
                return new PropertyNative<RectangleF>(propertyInfo, PropertyType.Vector4);
            else if (type == typeof(Color4))
                return new PropertyNative<Color4>(propertyInfo, PropertyType.Vector4);
            else if (type == typeof(Matrix3x2))
                return new PropertyNative<Matrix3x2>(propertyInfo, PropertyType.Matrix3x2);
            else if (type == typeof(Matrix4x3))
                return new PropertyNative<Matrix4x3>(propertyInfo, PropertyType.Matrix4x3);
            else if (type == typeof(Matrix4x4))
                return new PropertyNative<Matrix4x4>(propertyInfo, PropertyType.Matrix4x4);
            else if (type == typeof(Matrix5x4))
                return new PropertyNative<Matrix5x4>(propertyInfo, PropertyType.Matrix5x4);
            else if (type.IsEnum)
                return new PropertyNative<int>(propertyInfo, PropertyType.Enum);
            else
                return null;
        }

        private int SetterImpl(IntPtr thisPtr, IntPtr dataPtr, int dataSize)
        {
            if (dataPtr == IntPtr.Zero)
                return Result.Ok.Code;

            var shadow = ToShadow(thisPtr);
            var callback = (ID2D1EffectImpl)shadow.Callback;

            var value = Marshal.PtrToStructure<U>(dataPtr);
            PropertyInfo.SetValue(callback, value);

            return Result.Ok.Code;
        }

        private int GetterImpl(IntPtr thisPtr, IntPtr dataPtr, int datasize, out int actualSize)
        {
            actualSize = Marshal.SizeOf<U>();
            if (dataPtr == IntPtr.Zero)
                return Result.Ok.Code;

            var shadow = ToShadow(thisPtr);
            var callback = (ID2D1EffectImpl)shadow.Callback;

            var value = (U)PropertyInfo.GetValue(callback);
            Marshal.StructureToPtr(value, dataPtr, true);

            return Result.Ok.Code;
        }

        private ID2D1EffectImplShadow ToShadow(IntPtr ptr)
        {
            unsafe
            {
                return (ID2D1EffectImplShadow)GCHandle.FromIntPtr(*(((IntPtr*)ptr) + 1)).Target;
            }
        }
    }
}
