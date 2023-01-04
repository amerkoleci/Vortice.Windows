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

using System.Runtime.InteropServices;

namespace Vortice.DirectInput;

/// <summary>
/// Enumerator callback for DirectInput EnumEffectsInFile.
/// </summary>
internal class EnumEffectsInFileCallback
{
    private readonly DirectInputEnumEffectsInFileDelegate _callback;

    /// <summary>
    /// Initializes a new instance of the <see cref="EnumEffectsInFileCallback"/> class.
    /// </summary>
    public EnumEffectsInFileCallback()
    {
        unsafe
        {
            _callback = new DirectInputEnumEffectsInFileDelegate(DirectInputEnumEffectsInFileImpl);
            NativePointer = Marshal.GetFunctionPointerForDelegate(_callback);
            EffectsInFile = new List<EffectFile>();
        }
    }

    /// <summary>
    /// Natives the pointer.
    /// </summary>
    public IntPtr NativePointer { get; }

    /// <summary>
    /// Gets or sets the effects in file.
    /// </summary>
    public List<EffectFile> EffectsInFile { get; }

    // BOOL DIEnumEffectsInFileCallback(LPCDIEffectInfo pdei,LPVOID pvRef)
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    private unsafe delegate int DirectInputEnumEffectsInFileDelegate(void* deviceInstance, IntPtr data);
    private unsafe int DirectInputEnumEffectsInFileImpl(void* deviceInstance, IntPtr data)
    {
        var newEffect = new EffectFile();
        newEffect.__MarshalFrom(ref *((EffectFile.__Native*)deviceInstance));
        EffectsInFile.Add(newEffect);
        // Return true to continue iterating
        return 1;
    }
}
