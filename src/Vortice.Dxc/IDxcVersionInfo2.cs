// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

namespace Vortice.Dxc
{
    public partial class IDxcVersionInfo
    {
        public int GetFlags()
        {
            GetFlags(out int result).CheckError();
            return result;
        }
    }
}
