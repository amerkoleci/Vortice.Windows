// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

namespace SharpShaderCompiler.D3D11
{
    public partial class ID3D11ShaderReflection
    {
        private ShaderParameterDescription[] _inputParameters;
        private ShaderParameterDescription[] _outputParameters;
        private ID3D11ShaderReflectionConstantBuffer[] _constantBuffers;
        private InputBindingDescription[] _resources;

        public ShaderParameterDescription[] InputParameters
        {
            get
            {
                if (_inputParameters == null)
                {
                    _inputParameters = new ShaderParameterDescription[Description.InputParameters];
                    for (var i = 0; i < Description.InputParameters; i++)
                    {
                        _inputParameters[i] = GetInputParameterDesc(i);
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
                    for (var i = 0; i < Description.OutputParameters; i++)
                    {
                        _outputParameters[i] = GetOutputParameterDesc(i);
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
                    for (var i = 0; i < Description.ConstantBuffers; i++)
                    {
                        _constantBuffers[i] = GetConstantBufferByIndex(i);
                    }
                }

                return _constantBuffers;
            }
        }

        public InputBindingDescription[] Resources
        {
            get
            {
                if (_resources == null)
                {
                    _resources = new InputBindingDescription[Description.BoundResources];
                    for (var i = 0; i < Description.BoundResources; i++)
                    {
                        _resources[i] = GetResourceBindingDesc(i);
                    }
                }

                return _resources;
            }
        }
    }
}
