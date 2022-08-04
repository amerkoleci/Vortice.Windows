// Copyright © Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.Dxc;

public readonly struct DxcShaderModel : IEquatable<DxcShaderModel>
{
    public static readonly DxcShaderModel Model6_0 = new(6, 0);
    public static readonly DxcShaderModel Model6_1 = new(6, 1);
    public static readonly DxcShaderModel Model6_2 = new(6, 2);
    public static readonly DxcShaderModel Model6_3 = new(6, 3);
    public static readonly DxcShaderModel Model6_4 = new(6, 4);
    public static readonly DxcShaderModel Model6_5 = new(6, 5);
    public static readonly DxcShaderModel Model6_6 = new(6, 6);
    public static readonly DxcShaderModel Model6_7 = new(6, 7);

    public DxcShaderModel(int major, int minor)
    {
        Major = major;
        Minor = minor;
    }

    public int Major { get; }
    public int Minor { get; }

    /// <inheritdoc/>
    public override bool Equals(object? obj) => obj is DxcShaderModel value && Equals(value);

    public bool Equals(DxcShaderModel other)
    {
        return Major == other.Major && Minor == other.Minor;
    }

    public static bool operator ==(DxcShaderModel left, DxcShaderModel right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(DxcShaderModel left, DxcShaderModel right)
    {
        return !(left == right);
    }

    /// <inheritdoc/>
    public override int GetHashCode()
    {
        unchecked
        {
            int hashCode = Major.GetHashCode();
            hashCode = (hashCode * 397) ^ Minor.GetHashCode();
            return hashCode;
        }
    }

    /// <inheritdoc/>
    public override string ToString() => $"Major={Major}, Minor={Minor}";
}
