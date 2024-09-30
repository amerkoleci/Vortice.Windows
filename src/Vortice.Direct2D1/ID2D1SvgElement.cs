// Copyright (c) Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using System.Collections;

namespace Vortice.Direct2D1;

public unsafe partial class ID2D1SvgElement : IEnumerable<ID2D1SvgElement>
{
    public string TextValue
    {
        get
        {
            uint length = GetTextValueLength();
            char* chars = stackalloc char[(int)length + 1];
            GetTextValue(chars, length + 1).CheckError();
            return new string(chars, 0, (int)length);
        }
    }

    public string TagName
    {
        get
        {
            uint length = GetTagNameLength();
            char* chars = stackalloc char[(int)length + 1];
            GetTagName(chars, length + 1).CheckError();
            return new string(chars, 0, (int)length);
        }
    }

    public IEnumerator<ID2D1SvgElement> GetEnumerator()
    {
        ID2D1SvgElement? child = GetFirstChild();
        while (child != null)
        {
            yield return child;
            child = child.GetNextChild(child);
        }
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public IEnumerable<ID2D1SvgElement> DescendantsAndSelf()
    {
        yield return this;
        foreach (ID2D1SvgElement child in this)
        {
            foreach (ID2D1SvgElement descendant in child.DescendantsAndSelf())
            {
                yield return descendant;
            }
        }
    }

    public IEnumerable<ID2D1SvgElement> Descendants()
    {
        foreach (ID2D1SvgElement child in this)
        {
            yield return child;
            foreach (ID2D1SvgElement grandchild in child.Descendants())
            {
                yield return grandchild;
            }
        }
    }

    public IEnumerable<string?> AttributeNames
    {
        get
        {
            uint count = GetSpecifiedAttributeCount();
            for (uint i = 0; i < count; i++)
            {
                yield return GetSpecifiedAttributeName(i);
            }
        }
    }

    public unsafe string? GetSpecifiedAttributeName(uint index)
    {
        GetSpecifiedAttributeNameLength(index, out uint nameLength, out _);
        char* namePtr = (char*)NativeMemory.Alloc((nuint)nameLength, (nuint)sizeof(char));
        try
        {
            Result result = GetSpecifiedAttributeName(index, namePtr, nameLength, out _);
            if (result.Failure)
                return default;

            return new string(namePtr, 0, (int)nameLength);
        }
        finally
        {
            NativeMemory.Free(namePtr);
        }
    }

    public unsafe string? GetSpecifiedAttributeName(uint index, out bool inherited)
    {
        GetSpecifiedAttributeNameLength(index, out uint nameLength, out RawBool inheritedResult);
        char* namePtr = (char*)NativeMemory.Alloc((nuint)nameLength, (nuint)sizeof(char));
        try
        {
            Result result = GetSpecifiedAttributeName(index, namePtr, nameLength, out inheritedResult);
            if (result.Failure)
            {
                inherited = inheritedResult;
                return default;
            }

            inherited = inheritedResult;
            return new string(namePtr, 0, (int) nameLength);
        }
        finally
        {
            NativeMemory.Free(namePtr);
        }
    }

    public Result SetAttributeValue(string name, float value)
    {
        return SetAttributeValue(name, SvgAttributePodType.Float, &value, (uint)sizeof(float));
    }

    public Result SetAttributeValue(string name, Color4 value)
    {
        return SetAttributeValue(name, SvgAttributePodType.Color, (float*)&value, (uint)sizeof(Color4));
    }

    public Result SetAttributeValue(string name, FillMode value)
    {
        return SetAttributeValue(name, SvgAttributePodType.FillMode, &value, (uint)sizeof(FillMode));
    }

    public Result SetAttributeValue(string name, SvgDisplay value)
    {
        return SetAttributeValue(name, SvgAttributePodType.Display, &value, (uint)sizeof(SvgDisplay));
    }

    public Result SetAttributeValue(string name, SvgOverflow value)
    {
        return SetAttributeValue(name, SvgAttributePodType.Overflow, &value, (uint)sizeof(SvgOverflow));
    }

    public Result SetAttributeValue(string name, SvgLineJoin value)
    {
        return SetAttributeValue(name, SvgAttributePodType.LineJoin, &value, (uint)sizeof(SvgLineJoin));
    }

    public Result SetAttributeValue(string name, SvgLineCap value)
    {
        return SetAttributeValue(name, SvgAttributePodType.LineCap, &value, (uint)sizeof(SvgLineCap));
    }

    public Result SetAttributeValue(string name, SvgVisibility value)
    {
        return SetAttributeValue(name, SvgAttributePodType.Visibility, &value, (uint)sizeof(SvgVisibility));
    }

    public Result SetAttributeValue(string name, Matrix3x2 value)
    {
        return SetAttributeValue(name, SvgAttributePodType.Matrix, (float*)&value, (uint)sizeof(Matrix3x2));
    }

    public Result SetAttributeValue(string name, SvgUnitType value)
    {
        return SetAttributeValue(name, SvgAttributePodType.UnitType, &value, (uint)sizeof(SvgUnitType));
    }

    public Result SetAttributeValue(string name, ExtendMode value)
    {
        return SetAttributeValue(name, SvgAttributePodType.ExtendMode, &value, (uint)sizeof(ExtendMode));
    }

    public Result SetAttributeValue(string name, SvgPreserveAspectRatio value)
    {
        return SetAttributeValue(name, SvgAttributePodType.PreserveAspectRatio, &value, (uint)sizeof(SvgPreserveAspectRatio));
    }

    public Result SetAttributeValue(string name, SvgViewbox value)
    {
        return SetAttributeValue(name, SvgAttributePodType.Viewbox, &value, (uint)sizeof(SvgViewbox));
    }

    public Result SetAttributeValue(string name, SvgLength value)
    {
        return SetAttributeValue(name, SvgAttributePodType.Length, &value, (uint)sizeof(SvgLength));
    }

    public Result SetAttributeValue<T>(string name, SvgAttributePodType type, in T value) where T : unmanaged
    {
        fixed (T* pValue = &value)
        {
            return SetAttributeValue(name, type, (IntPtr)pValue, (uint)sizeof(T));
        }
    }

    private Result SetAttributeValue(string name, SvgAttributePodType type, void* value, uint valueSizeInBytes)
    {
        return SetAttributeValue(name, type, (IntPtr)value, valueSizeInBytes);
    }
}
