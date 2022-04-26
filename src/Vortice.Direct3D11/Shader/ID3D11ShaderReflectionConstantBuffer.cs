// Copyright © Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.Direct3D11.Shader;

public partial class ID3D11ShaderReflectionConstantBuffer
{
    private ID3D11ShaderReflectionVariable[] _variables;

    public ID3D11ShaderReflectionVariable[] Variables
    {
        get
        {
            if (_variables == null)
            {
                _variables = new ID3D11ShaderReflectionVariable[Description.VariableCount];
                for (int i = 0; i < Description.VariableCount; i++)
                {
                    _variables[i] = GetVariableByIndex(i);
                }
            }

            return _variables;
        }
    }
}
