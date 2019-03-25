// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using SharpGen.Runtime.Win32;

namespace SharpDirect2D.WIC
{
    public partial class IWICBitmapEncoder
    {
        public void Initialize(IStream stream)
        {
            Initialize(stream, BitmapEncoderCacheOption.NoCache);
        }
    }
}
