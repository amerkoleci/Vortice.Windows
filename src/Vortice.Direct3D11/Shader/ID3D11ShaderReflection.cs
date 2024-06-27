// Copyright (c) Amer Koleci and contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using Vortice.Mathematics;

namespace Vortice.Direct3D11.Shader;

public partial class ID3D11ShaderReflection
{
    private ShaderParameterDescription[] _inputParameters;
    private ShaderParameterDescription[] _outputParameters;
    private ID3D11ShaderReflectionConstantBuffer[] _constantBuffers;
    private InputBindingDescription[] _boundResources;
    private ShaderParameterDescription[] _patchConstantParameters;

    public Int3 ThreadGroupSize
    {
        get
        {
            GetThreadGroupSize(out int x, out int y, out int z);
            return new Int3(x, y, z);
        }
    }

    public ShaderParameterDescription[] InputParameters
    {
        get
        {
            if (_inputParameters == null)
            {
                _inputParameters = new ShaderParameterDescription[Description.InputParameters];
                for (int i = 0; i < Description.InputParameters; i++)
                {
                    _inputParameters[i] = GetInputParameterDescription(i);
                }
            }

            return _inputParameters;
        }
    }

    public ShaderParameterDescription[] OutputParameters
    {
        get
        {
            if (_outputParameters == null)
            {
                _outputParameters = new ShaderParameterDescription[Description.OutputParameters];
                for (int i = 0; i < Description.OutputParameters; i++)
                {
                    _outputParameters[i] = GetOutputParameterDescription(i);
                }
            }

            return _outputParameters;
        }
    }

    public ID3D11ShaderReflectionConstantBuffer[] ConstantBuffers
    {
        get
        {
            if (_constantBuffers == null)
            {
                _constantBuffers = new ID3D11ShaderReflectionConstantBuffer[Description.ConstantBuffers];
                for (int i = 0; i < Description.ConstantBuffers; i++)
                {
                    _constantBuffers[i] = GetConstantBufferByIndex(i);
                }
            }

            return _constantBuffers;
        }
    }

    public InputBindingDescription[] BoundResources
    {
        get
        {
            if (_boundResources == null)
            {
                _boundResources = new InputBindingDescription[Description.BoundResources];
                for (int i = 0; i < Description.BoundResources; i++)
                {
                    _boundResources[i] = GetResourceBindingDescription(i);
                }
            }

            return _boundResources;
        }
    }

    public ShaderParameterDescription[] PatchConstantParameters
    {
        get
        {
            if (_patchConstantParameters == null)
            {
                _patchConstantParameters = new ShaderParameterDescription[Description.PatchConstantParameters];
                for (int i = 0; i < Description.BoundResources; i++)
                {
                    _patchConstantParameters[i] = GetPatchConstantParameterDescription(i);
                }
            }

            return _patchConstantParameters;
        }
    }

    public int GetThreadGroupSize(out Int3 size)
    {
        int totalSize = GetThreadGroupSize(out int x, out int y, out int z);
        size = new Int3(x, y, z);
        return totalSize;
    }
}
