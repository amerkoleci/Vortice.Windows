// Copyright (c) Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.UIAnimation;

public partial class IUIAnimationTransitionFactory2
{
    private static readonly Guid CLSID_IUIAnimationTransitionFactory2 = new("84302F97-7F7B-4040-B190-72AC9D18E420");

    /// <summary>
    /// Create a new instance of the <see cref="IUIAnimationTransitionFactory2"/> class.
    /// </summary>
    public IUIAnimationTransitionFactory2()
    {
        ComUtilities.CreateComInstance
            (CLSID_IUIAnimationTransitionFactory2,
            ComContext.InprocServer,
            typeof(IUIAnimationTransitionFactory2).GUID,
            this);
    }
}
