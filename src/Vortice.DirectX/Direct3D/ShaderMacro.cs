// Copyright (c) Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using static Vortice.UnsafeUtilities;

namespace Vortice.Direct3D;

public partial struct ShaderMacro : IEquatable<ShaderMacro>
{
    /// <unmanaged>D3D_SHADER_MACRO::Name</unmanaged>
    /// <unmanaged-short>Name</unmanaged-short>
    public string? Name;
    /// <unmanaged>D3D_SHADER_MACRO::Definition</unmanaged>
    /// <unmanaged-short>Definition</unmanaged-short>
    public string? Definition;

    /// <summary>
    /// Initializes a new instance of the <see cref="ShaderMacro"/> struct. 
    /// </summary>
    /// <param name="name">The macro name.</param>
    /// <param name="definition">The macro definition.</param>
    public ShaderMacro(string? name, object? definition)
    {
        Name = name;
        Definition = definition?.ToString();
    }

    public readonly bool Equals(ShaderMacro other)
    {
        return string.Equals(Name, other.Name) && string.Equals(Definition, other.Definition);
    }

    /// <inheritdoc/>
    public override readonly bool Equals(object? obj) => obj is ShaderMacro value && Equals(value);

    public override readonly int GetHashCode() => HashCode.Combine(Name, Definition);

    public static bool operator ==(ShaderMacro left, ShaderMacro right) => left.Equals(right);

    public static bool operator !=(ShaderMacro left, ShaderMacro right) => !left.Equals(right);

    #region Marshal
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    internal unsafe struct __Native
    {
        public nint Name;
        public nint Definition;
    }

    internal readonly unsafe void __MarshalTo(ref __Native @ref)
    {
        @ref.Name = Marshal.StringToHGlobalAnsi(Name);
        @ref.Definition = Marshal.StringToHGlobalAnsi(Definition);
    }

    internal readonly unsafe void __MarshalFree(ref __Native @ref)
    {
        Free(@ref.Name);
        Free(@ref.Definition);
    }
    #endregion Marshal
}
