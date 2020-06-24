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
        /// <summary>
        /// Get the number of available outputs from this adapter.
        /// </summary>
        /// <returns>The number of outputs</returns>
        public virtual int GetOutputCount()
        {
            int count = 0;
            while (true)
            {
                var result = EnumOutputs(count, out var output);
                if (result == ResultCode.NotFound || output == null)
                    break;

                output.Dispose();
                count++;
            }

            return count;
        }

        /// <summary>
        /// Get an instance of <see cref="IDXGIAdapter"/> or null if not found.
        /// </summary>
        /// <remarks>
        /// Make sure to dispose the <see cref="IDXGIAdapter"/> instance.
        /// </remarks>
        /// <param name="index">The index to get from.</param>
        /// <returns>Instance of <see cref="IDXGIAdapter"/> or null if not found.</returns>
        public IDXGIOutput GetOutput(int index)
        {
            var result = EnumOutputs(index, out var adapter);
            if (result.Failure)
            {
                return null;
            }

            return adapter;
        }

        /// <summary>
        /// Enumerates all outputs from this adapter.
        /// </summary>
        /// <remarks>
        /// Make sure to dispose the array instance, using <see cref="Utilities.Dispose{T}(T[])"/>
        /// </remarks>
        /// <returns>An array of <see cref="IDXGIOutput"/></returns>
        public IDXGIOutput[] EnumOutputs()
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

            return outputs.ToArray();
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
    }
}
