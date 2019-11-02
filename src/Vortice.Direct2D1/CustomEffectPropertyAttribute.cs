using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace Vortice.Direct2D1
{
    [AttributeUsage(AttributeTargets.Property,Inherited = true)]
    public class CustomEffectPropertyAttribute : Attribute
    {
        public PropertyType PropertyType { get; }
        public int Order { get; }
        public CustomEffectPropertyAttribute(PropertyType propertyType, int order)
        {
            PropertyType = propertyType;
            Order = order;
        }
    }
}
