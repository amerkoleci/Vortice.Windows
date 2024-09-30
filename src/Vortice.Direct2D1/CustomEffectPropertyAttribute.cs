// Copyright (c) Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.Direct2D1;

[AttributeUsage(AttributeTargets.Property, Inherited = true)]
public class CustomEffectPropertyAttribute : Attribute
{
    public CustomEffectPropertyAttribute(PropertyType propertyType, int order)
    {
        PropertyType = propertyType;
        Order = order;
    }

    public PropertyType PropertyType { get; }
    public int Order { get; }
}
