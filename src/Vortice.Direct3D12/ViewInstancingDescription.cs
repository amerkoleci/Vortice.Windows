// Copyright © Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using System.Runtime.CompilerServices;

namespace Vortice.Direct3D12;

/// <summary>
/// Specifies parameters used during view instancing configuration.
/// </summary>
public partial class ViewInstancingDescription
{
    public ViewInstancingDescription(ViewInstancingFlags flags = ViewInstancingFlags.None)
    {
        Flags = flags;
    }

    public ViewInstancingDescription(params ViewInstanceLocation[] locations)
    {
        Locations = locations;
    }

    /// <summary>	
    /// An array of <see cref="ViewInstanceLocation"/> that specify the location of viewport/scissor and render target details of each view instance.
    /// </summary>	
    public ViewInstanceLocation[]? Locations { get; set; }

    /// <summary>
    /// Configures view instancing with additional options.
    /// </summary>
    public ViewInstancingFlags Flags { get; set; }

    /// <summary>
    /// Implicitely converts to an <see cref="ViewInstancingDescription"/> from an array of <see cref="ViewInstanceLocation"/>
    /// </summary>
    /// <param name="locations">Array of <see cref="ViewInstanceLocation"/>.</param>
    public static implicit operator ViewInstancingDescription(ViewInstanceLocation[] locations)
    {
        return new ViewInstancingDescription(locations);
    }

    #region Marshal
    [StructLayout(LayoutKind.Sequential, Pack = 0)]
    internal unsafe struct __Native
    {
        public int ViewInstanceCount;
        public ViewInstanceLocation* pViewInstanceLocations;
        public ViewInstancingFlags Flags;
    }

    internal unsafe void __MarshalFree(ref __Native @ref)
    {
    }

    internal unsafe void __MarshalTo(ref __Native @ref)
    {
        @ref.ViewInstanceCount = Locations?.Length ?? 0;
        if (@ref.ViewInstanceCount > 0)
        {
            @ref.pViewInstanceLocations = (ViewInstanceLocation*)Unsafe.AsPointer(ref Locations![0]);
        }
        @ref.Flags = Flags;
    }
    #endregion
}
