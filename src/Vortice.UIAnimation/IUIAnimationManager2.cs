// Copyright © Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.UIAnimation;

public partial class IUIAnimationManager2
{
    private static readonly Guid CLSID_UIAnimationManager2 = new("D25D8842-8884-4A4A-B321-091314379BDD");

    /// <summary>
    /// Create a new instance of the <see cref="IUIAnimationManager2"/> class.
    /// </summary>
    public IUIAnimationManager2()
    {
        ComUtilities.CreateComInstance(CLSID_UIAnimationManager2,
            ComContext.InprocServer,
            typeof(IUIAnimationManager2).GUID,
            this);
    }
}
