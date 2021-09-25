// Copyright (c) 2010-2014 SharpDX - Alexandre Mutel
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using SharpGen.Runtime;

namespace Vortice.DirectInput
{
    public partial class IDirectInputEffect
    {
        /// <summary>
        /// Sends a hardware-specific command to the force-feedback driver.
        /// </summary>
        /// <param name="command">Driver-specific command number. Consult the driver documentation for a list of valid commands. </param>
        /// <param name="inData">Buffer containing the data required to perform the operation. </param>
        /// <param name="outData">Buffer in which the operation's output data is returned. </param>
        /// <returns>Number of bytes written to the output buffer</returns>
        /// <remarks>
        /// Because each driver implements different escapes, it is the application's responsibility to ensure that it is sending the escape to the correct driver by comparing the value of the guidFFDriver member of the <see cref="DeviceInstance"/> structure against the value the application is expecting.
        /// </remarks>
        public unsafe int Escape(int command, byte[] inData, byte[] outData)
        {
            var effectEscape = new EffectEscape();
            fixed (void* pInData = &inData[0])
            fixed (void* pOutData = &outData[0])
            {
                effectEscape.Command = command;
                effectEscape.InBufferPointer = (IntPtr)pInData;
                effectEscape.InBufferSize = inData.Length;
                effectEscape.OutBufferPointer = (IntPtr)pOutData;
                effectEscape.OutBufferSize = outData.Length;

                Escape(ref effectEscape);
                return effectEscape.OutBufferSize;
            }
        }

        /// <summary>
        /// Gets the characteristics of an effect.
        /// </summary>
        /// <returns>The current parameters of this effect.</returns>
        public EffectParameters GetParameters() => GetParameters(EffectParameterFlags.All);

        /// <summary>
        /// Sets the characteristics of an effect.
        /// </summary>
        /// <param name="parameters">The parameters of this effect.</param>
        /// <returns>A <see cref = "T:Vortice.Result" /> object describing the result of the operation.</returns>
        public void SetParameters(EffectParameters parameters) => SetParameters(parameters, EffectParameterFlags.All);

        /// <summary>
        /// Begins playing an effect infinitely. If the effect is already playing, it is restarted from the beginning. If the effect has not been downloaded or has been modified since its last download, it is downloaded before being started. This default behavior can be suppressed by passing the <see cref="EffectPlayFlags.NoDownload"/> flag.
        /// </summary>
        /// <returns>A <see cref="Result" /> object describing the result of the operation.</returns>
        public Result Start() => Start(-1);

        /// <summary>
        /// Begins playing an effect infinitely. If the effect is already playing, it is restarted from the beginning. If the effect has not been downloaded or has been modified since its last download, it is downloaded before being started. This default behavior can be suppressed by passing the <see cref="EffectPlayFlags.NoDownload"/> flag.
        /// </summary>
        /// <param name="flags">Flags that describe how the effect should be played by the device.</param>
        /// <returns>A <see cref="Result" /> object describing the result of the operation.</returns>
        public Result Start(EffectPlayFlags flags) => Start(-1, flags);

        /// <summary>
        /// Begins playing an effect. If the effect is already playing, it is restarted from the beginning. If the effect has not been downloaded or has been modified since its last download, it is downloaded before being started. This default behavior can be suppressed by passing the <see cref="EffectPlayFlags.NoDownload"/> flag.
        /// </summary>
        /// <param name="iterations">Number of times to play the effect in sequence. The envelope is re-articulated with each iteration. To play the effect exactly once, pass 1. To play the effect repeatedly until explicitly stopped, pass -1. To play the effect until explicitly stopped without re-articulating the envelope, modify the effect parameters with the <see cref="SetParameters(Vortice.DirectInput.EffectParameters,Vortice.DirectInput.EffectParameterFlags)"/>method, and change the Duration member to -1. </param>
        /// <returns>A <see cref="Result" /> object describing the result of the operation.</returns>
        public Result Start(int iterations) => Start(iterations, EffectPlayFlags.None);
    }
}
