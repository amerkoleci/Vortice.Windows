// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using System.Runtime.CompilerServices;

namespace Vortice
{
    public interface IGraphicsDevice : IDisposable
    {
        bool DrawFrame(Action<int, int> draw, [CallerMemberName]string frameName = null);
    }
}
