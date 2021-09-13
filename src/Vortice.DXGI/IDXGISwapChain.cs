// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using SharpGen.Runtime;

namespace Vortice.DXGI
{
    public partial class IDXGISwapChain
    {
        public bool IsFullscreen
        {
            get
            {
                GetFullscreenState(out RawBool fullscreen, out _).CheckError();
                return fullscreen;
            }
        }

        public T GetBuffer<T>(int index) where T : ComObject
        {
            GetBuffer(index, typeof(T).GUID, out IntPtr nativePtr).CheckError();
            return MarshallingHelpers.FromPointer<T>(nativePtr);
        }

        public Result GetBuffer<T>(int index, out T? surface) where T : ComObject
        {
            Result result = GetBuffer(index, typeof(T).GUID, out IntPtr nativePtr);
            if (result.Failure)
            {
                surface = default;
                return result;
            }

            surface = MarshallingHelpers.FromPointer<T>(nativePtr);
            return result;
        }

        public Result GetFullscreenState(out RawBool fullscreen) => GetFullscreenState(out fullscreen, out _);

        public Result SetFullscreenState(bool fullscreen, IDXGIOutput? target = default) => SetFullscreenState(new RawBool(fullscreen), target);

        public Result ResizeTarget(ModeDescription newTargetParameters) => ResizeTarget(ref newTargetParameters);

        public Result ResizeBuffers(int bufferCount, int width, int height, Format newFormat = Format.Unknown)
        {
            return ResizeBuffers(bufferCount, width, height, newFormat, SwapChainFlags.None);
        }

        public Result Present(int syncInterval) => Present(syncInterval, PresentFlags.None);
    }
}
