// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using SharpGen.Runtime;

namespace Vortice.DXGI
{
    public partial class IDXGISwapChain
    {
        public T? GetBuffer<T>(int index) where T : ComObject
        {
            GetBuffer(index, out T? surface);
            return surface;
        }

        public Result GetBuffer<T>(int index, out T? surface) where T : ComObject
        {
            var result = GetBuffer(index, typeof(T).GUID, out IntPtr nativePtr);
            if (result.Failure)
            {
                surface = default;
                return result;
            }

            surface = MarshallingHelpers.FromPointer<T>(nativePtr);
            return result;
        }

        public void GetFullscreenState(out RawBool fullscreen)
        {
            GetFullscreenState(out fullscreen, out _);
        }

        public Result SetFullscreenState(bool fullscreen, IDXGIOutput? target = default)
        {
            return SetFullscreenState(new RawBool(fullscreen), target);
        }

        public Result ResizeTarget(ModeDescription newTargetParameters)
        {
            return ResizeTarget(ref newTargetParameters);
        }
    }
}
