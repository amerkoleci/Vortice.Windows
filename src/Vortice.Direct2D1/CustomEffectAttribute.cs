// Copyright (c) Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.Direct2D1;

[AttributeUsage(AttributeTargets.Class, Inherited = true)]
public class CustomEffectAttribute : Attribute
{
    public CustomEffectAttribute(string[] inputs, string? displayName = null, string? description = null, string? category = null, string? author = null)
    {
        Inputs = inputs;
        DisplayName = displayName ?? string.Empty;
        Description = description ?? string.Empty;
        Category = category ?? string.Empty;
        Author = author ?? string.Empty;
    }

    public CustomEffectAttribute(int inputCount, string? displayName = null, string? description = null, string? category = null, string? author = null)
    {
        Inputs = new string[inputCount];
        for (int i = 0; i < inputCount; i++)
        {
            Inputs[i] = $"Input{i}";
        }
        DisplayName = displayName ?? string.Empty;
        Description = description ?? string.Empty;
        Category = category ?? string.Empty;
        Author = author ?? string.Empty;
    }

    public string DisplayName { get; }
    public string Description { get; }
    public string Category { get; }
    public string Author { get; }
    public string[] Inputs { get; }
}
