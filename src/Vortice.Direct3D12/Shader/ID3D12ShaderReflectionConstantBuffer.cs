// Copyright (c) Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.Direct3D12.Shader;

public partial class ID3D12ShaderReflectionConstantBuffer
{
    private ID3D12ShaderReflectionVariable[] _variables;

    public ID3D12ShaderReflectionVariable[] Variables
    {
        get
        {
            if (_variables == null)
            {
                _variables = new ID3D12ShaderReflectionVariable[Description.VariableCount];
                for (uint i = 0; i < Description.VariableCount; i++)
                {
                    _variables[i] = GetVariableByIndex(i);
                }
            }

            return _variables;
        }
    }
}
