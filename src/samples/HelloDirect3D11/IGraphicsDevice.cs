// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;

namespace HelloDirect3D11
{
    public interface IGraphicsDevice : IDisposable
    {
        void Present();
    }
}
