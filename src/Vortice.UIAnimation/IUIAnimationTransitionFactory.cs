// Copyright © Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.UIAnimation;

public partial class IUIAnimationTransitionFactory
{
    private static readonly Guid CLSID_IUIAnimationTransitionFactory = new("8A9B1CDD-FCD7-419c-8B44-42FD17DB1887");

    /// <summary>
    /// Create a new instance of the <see cref="IUIAnimationTransitionFactory"/> class.
    /// </summary>
    public IUIAnimationTransitionFactory()
    {
        ComUtilities.CreateComInstance(CLSID_IUIAnimationTransitionFactory,
            ComContext.InprocServer,
            typeof(IUIAnimationTransitionFactory).GUID,
            this);
    }
}
