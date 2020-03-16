// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using SharpGen.Runtime;

namespace Vortice.DXGI
{
    public partial class IDXGIAdapter
    {
        private ReadOnlyCollection<IDXGIOutput> _outputs;

        public ReadOnlyCollection<IDXGIOutput> Outputs
        {
            get
            {
                if (_outputs == null)
                {
                    var outputs = new List<IDXGIOutput>();
                    while (true)
                    {
                        var result = EnumOutputs(outputs.Count, out var output);
                        if (result == ResultCode.NotFound || output == null)
                        {
                            break;
                        }

                        outputs.Add(output);
                    }

                    _outputs = new ReadOnlyCollection<IDXGIOutput>(outputs);
                }

                return _outputs;
            }
        }

        public bool CheckInterfaceSupport<T>() where T : ComObject
        {
            return CheckInterfaceSupport(typeof(T), out _);
        }

        public bool CheckInterfaceSupport<T>(out long userModeVersion) where T : ComObject
        {
            return CheckInterfaceSupport(typeof(T), out userModeVersion);
        }

        public bool CheckInterfaceSupport(Type type, out long userModeDriverVersion)
        {
            return CheckInterfaceSupport(type.GUID, out userModeDriverVersion).Success;
        }

        /// <inheritdoc/>
        protected override unsafe void Dispose(bool disposing)
        {
            if (disposing)
            {
                ReleaseOutputs();
            }

            base.Dispose(disposing);
        }

        private void ReleaseOutputs()
        {
            if (_outputs == null)
                return;

            var outputsCount = _outputs.Count;
            for (var i = 0; i < outputsCount; i++)
            {
                _outputs[i].Release();
            }
        }
    }
}
