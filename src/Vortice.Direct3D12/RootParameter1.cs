// Copyright (c) Amer Koleci and contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.Direct3D12;

/// <summary>
/// Describes the slot of a root signature version 1.0.
/// </summary>
public partial struct RootParameter1
{
    private readonly RootParameterType _parameterType;
    private readonly Union _union;
    private readonly ShaderVisibility _shaderVisibility;

    public RootParameterType ParameterType => _parameterType;

    public RootDescriptorTable1 DescriptorTable
    {
        get
        {
            var result = new RootDescriptorTable1();
            result.__MarshalFrom(_union.DescriptorTable);
            return result;
        }
    }

    public RootConstants Constants
    {
        get => _union.Constants;
    }

    public RootDescriptor1 Descriptor
    {
        get => _union.Descriptor;
    }

    public ShaderVisibility ShaderVisibility => _shaderVisibility;

    /// <summary>
    /// Initializes a new instance of the <see cref="RootParameter1"/> struct.
    /// </summary>
    /// <param name="descriptorTable">The descriptor table.</param>
    /// <param name="visibility">The shader visibility.</param>
    public RootParameter1(RootDescriptorTable1 descriptorTable, ShaderVisibility visibility)
    {
        _parameterType = RootParameterType.DescriptorTable;
        _union = new Union();
        descriptorTable.__MarshalTo(ref _union.DescriptorTable);
        _shaderVisibility = visibility;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="RootParameter1"/> struct.
    /// </summary>
    /// <param name="rootConstants">The root constants.</param>
    /// <param name="visibility">The shader visibility.</param>
    public RootParameter1(RootConstants rootConstants, ShaderVisibility visibility)
    {
        _parameterType = RootParameterType.Constant32Bits;
        _union = new Union { Constants = rootConstants };
        _shaderVisibility = visibility;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="RootParameter1"/> struct.
    /// </summary>
    /// <param name="parameterType">The type.</param>
    /// <param name="rootDescriptor">The root descriptor.</param>
    /// <param name="visibility">The visibility.</param>
    /// <exception cref="System.ArgumentException">type</exception>
    public RootParameter1(RootParameterType parameterType, RootDescriptor1 rootDescriptor, ShaderVisibility visibility)
    {
        if (parameterType == RootParameterType.Constant32Bits
            || parameterType == RootParameterType.DescriptorTable)
        {
            throw new ArgumentException($"Cannot this type [{parameterType}] for a RootDescriptor", nameof(parameterType));
        }

        _parameterType = parameterType;
        _union = new Union { Descriptor = rootDescriptor };
        _shaderVisibility = visibility;
    }

    #region Union
    [StructLayout(LayoutKind.Explicit, Pack = 0)]
    private struct Union
    {
        [FieldOffset(0)]
        public RootDescriptorTable1.__Native DescriptorTable;

        [FieldOffset(0)]
        public RootConstants Constants;

        [FieldOffset(0)]
        public RootDescriptor1 Descriptor;
    }
    #endregion
}
