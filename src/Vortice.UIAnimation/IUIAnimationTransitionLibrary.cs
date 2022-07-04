// Copyright © Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.UIAnimation;

public partial class IUIAnimationTransitionLibrary
{
    private static readonly Guid CLSID_UIAnimationTransitionLibrary = new("1D6322AD-AA85-4EF5-A828-86D71067D145");

    /// <summary>
    /// Create a new instance of the <see cref="IUIAnimationTransitionLibrary"/> class.
    /// </summary>
    public IUIAnimationTransitionLibrary()
    {
        ComUtilities.CreateComInstance(CLSID_UIAnimationTransitionLibrary,
            ComContext.InprocServer,
            typeof(IUIAnimationTransitionLibrary).GUID,
            this);
    }
}
