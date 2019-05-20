// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

namespace Vortice.DirectX.Direct3D12
{
    /// <summary>
    /// Describes depth-stencil state.
    /// </summary>
    public partial struct DepthStencilDescription1
    {
        /// <summary>
        /// A built-in description with default settings for using a depth stencil buffer.
        /// </summary>
        public static readonly DepthStencilDescription1 Default = new DepthStencilDescription1(true, true);

        /// <summary>
        /// A built-in description with settings for enabling a read-only depth stencil buffer.
        /// </summary>
        public static readonly DepthStencilDescription1 DepthRead = new DepthStencilDescription1(true, false);

        /// <summary>
        /// A built-in description with settings for not using a depth stencil buffer.
        /// </summary>
        public static readonly DepthStencilDescription1 None = new DepthStencilDescription1(false, false);

        /// <summary>
        /// Initializes a new instance of the <see cref="DepthStencilDescription1"/> struct.
        /// </summary>
        /// <param name="depthEnable"></param>
        /// <param name="depthWriteEnable"></param>
        public DepthStencilDescription1(bool depthEnable, bool depthWriteEnable)
        {
            DepthEnable = depthEnable;
            DepthWriteMask = depthWriteEnable ? DepthWriteMask.All : DepthWriteMask.Zero;
            DepthFunc = ComparisonFunction.Less;
            StencilEnable = false;
            StencilReadMask = DepthStencilDescription.DefaultStencilReadMask;
            StencilWriteMask = DepthStencilDescription.DefaultStencilWriteMask;
            FrontFace = DepthStencilOperationDescription.Default;
            BackFace = DepthStencilOperationDescription.Default;
            DepthBoundsTestEnable = false;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DepthStencilDescription1"/> struct.
        /// </summary>
        /// <param name="depthEnable">Specifies whether to enable depth testing. Set this member to <b>true</b> to enable depth testing.</param>
        /// <param name="depthWriteEnable">Specifies a value that identifies a portion of the depth-stencil buffer that can be modified by depth data.</param>
        /// <param name="depthFunc">A <see cref="ComparisonFunction"/> value that identifies a function that compares depth data against existing depth data.</param>
        /// <param name="stencilEnable">Specifies whether to enable stencil testing. Set this member to <b>true</b> to enable stencil testing.</param>
        /// <param name="stencilReadMask">Identify a portion of the depth-stencil buffer for reading stencil data.</param>
        /// <param name="stencilWriteMask">Identify a portion of the depth-stencil buffer for writing stencil data.</param>
        /// <param name="frontStencilFailOp"></param>
        /// <param name="frontStencilDepthFailOp"></param>
        /// <param name="frontStencilPassOp"></param>
        /// <param name="frontStencilFunc"></param>
        /// <param name="backStencilFailOp"></param>
        /// <param name="backStencilDepthFailOp"></param>
        /// <param name="backStencilPassOp"></param>
        /// <param name="backStencilFunc"></param>
        /// <param name="depthBoundsTestEnable"></param>
        public DepthStencilDescription1(
            bool depthEnable,
            bool depthWriteEnable,
            ComparisonFunction depthFunc,
            bool stencilEnable,
            byte stencilReadMask,
            byte stencilWriteMask,
            StencilOperation frontStencilFailOp,
            StencilOperation frontStencilDepthFailOp,
            StencilOperation frontStencilPassOp,
            ComparisonFunction frontStencilFunc,
            StencilOperation backStencilFailOp,
            StencilOperation backStencilDepthFailOp,
            StencilOperation backStencilPassOp,
            ComparisonFunction backStencilFunc,
            bool depthBoundsTestEnable)
        {
            DepthEnable = depthEnable;
            DepthWriteMask = depthWriteEnable ? DepthWriteMask.All : DepthWriteMask.Zero;
            DepthFunc = depthFunc;
            StencilEnable = stencilEnable;
            StencilReadMask = stencilReadMask;
            StencilWriteMask = stencilWriteMask;
            FrontFace.StencilFailOp = frontStencilFailOp;
            FrontFace.StencilDepthFailOp = frontStencilDepthFailOp;
            FrontFace.StencilPassOp = frontStencilPassOp;
            FrontFace.StencilFunc = frontStencilFunc;
            BackFace.StencilFailOp = backStencilFailOp;
            BackFace.StencilDepthFailOp = backStencilDepthFailOp;
            BackFace.StencilPassOp = backStencilPassOp;
            BackFace.StencilFunc = backStencilFunc;
            DepthBoundsTestEnable = depthBoundsTestEnable;
        }
    }
}
