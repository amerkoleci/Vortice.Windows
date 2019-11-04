// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;

namespace Vortice.Direct2D1
{
    [AttributeUsage(AttributeTargets.Property,Inherited = true)]
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
}
