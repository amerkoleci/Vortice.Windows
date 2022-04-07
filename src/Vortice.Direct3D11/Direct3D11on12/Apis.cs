// Copyright © Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using Vortice.Direct3D;
using Vortice.Direct3D11;

namespace Vortice.Direct3D11on12;

public static unsafe partial class Apis
{
    public static Result D3D11On12CreateDevice(
        IUnknown d3d12Device,
        DeviceCreationFlags flags,
        FeatureLevel[] featureLevels,
        IUnknown[] commandQueues,
        int nodeMask,
        out ID3D11Device device,
        out ID3D11DeviceContext immediateContext,
        out FeatureLevel chosenFeatureLevel)
    {
        Result result = D3D11On12CreateDevice(d3d12Device,
            flags,
            featureLevels, featureLevels.Length,
            commandQueues, commandQueues.Length,
            nodeMask,
            out device, out immediateContext, out chosenFeatureLevel);

        if (result.Failure)
        {
            return result;
        }

        return result;
    }
}
