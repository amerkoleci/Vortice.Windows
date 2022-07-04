// Copyright © Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.UIAnimation;

public partial class IUIAnimationTransitionLibrary2
{
    private static readonly Guid CLSID_UIAnimationTransitionLibrary2 = new("812F944A-C5C8-4CD9-B0A6-B3DA802F228D");

    /// <summary>
    /// Create a new instance of the <see cref="IUIAnimationTransitionLibrary2"/> class.
    /// </summary>
    public IUIAnimationTransitionLibrary2()
    {
        ComUtilities.CreateComInstance(CLSID_UIAnimationTransitionLibrary2,
            ComContext.InprocServer,
            typeof(IUIAnimationTransitionLibrary2).GUID,
            this);
    }
}
