// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using SharpGen.Runtime;

namespace Vortice.Dxc
{
    public partial class IDxcPdbUtils
    {
        public IDxcResult CompileForFullPDB()
        {
            CompileForFullPDB(out IDxcResult result).CheckError();
            return result;
        }
    }
}
