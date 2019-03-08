using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using SharpGen.Runtime;
using Vortice.Direct3D;
using Vortice.DXGI;

namespace Vortice.Direct3D11
{
    public partial class ID3D11DeviceContext
    {
        public ID3D11CommandList FinishCommandList(bool restoreState)
        {
            var result = new ID3D11CommandList();
            FinishCommandListInternal(restoreState, result).CheckError();
            return result;
        }

        public Result FinishCommandList(bool restoreState, ID3D11CommandList commandList)
        {
            return FinishCommandListInternal(restoreState, commandList);
        }
    }
}
