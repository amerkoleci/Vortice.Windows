// Copyright © Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using System.Diagnostics;

namespace Vortice.Direct3D;

/// <summary>
/// Shadow callback for <see cref="Include"/>.
/// </summary>
internal sealed class IncludeShadow : CppObjectShadow
{
    internal readonly Dictionary<IntPtr, Frame> _frames = new();

    internal struct Frame : IDisposable
    {
        public Frame(Stream stream, GCHandle handle)
        {
            Stream = stream;
            _handle = handle;
        }

        public readonly Stream Stream;
        private GCHandle _handle;

        public void Dispose()
        {
            if (_handle.IsAllocated)
                _handle.Free();
        }
    }
}
