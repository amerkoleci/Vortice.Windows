// Copyright © Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.UIAnimation;

public partial class IUIAnimationManager
{
    private static readonly Guid CLSID_UIAnimationManager = new("4C1FC63A-695C-47E8-A339-1A194BE3D0B8");

    /// <summary>
    /// Create a new instance of the <see cref="IUIAnimationManager"/> class.
    /// </summary>
    public IUIAnimationManager()
    {
        ComUtilities.CreateComInstance(CLSID_UIAnimationManager,
            ComContext.InprocServer,
            typeof(IUIAnimationManager).GUID,
            this);
    }
}
