// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;

namespace Vortice.Direct2D1
{
    [AttributeUsage(AttributeTargets.Class, Inherited = true)]
    public class CustomEffectAttribute : Attribute
    {
        public CustomEffectAttribute(string[] inputs, string displayName = null, string description = null, string category = null, string author = null)
        {
            Inputs = inputs;
            DisplayName = displayName;
            Description = description;
            Category = category;
            Author = author;
        }

        public CustomEffectAttribute(int inputCount, string displayName = null, string description = null, string category = null, string author = null)
        {
            Inputs = new string[inputCount];
            for(int i = 0; i < inputCount; i++)
            {
                Inputs[i] = $"Input{i}";
            }
            DisplayName = displayName;
            Description = description;
            Category = category;
            Author = author;
        }

        public string DisplayName { get; }
        public string Description { get; }
        public string Category { get; }
        public string Author { get; }
        public string[] Inputs { get; }
    }
}
