// Copyright (c) Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.UIAnimation;

public partial class IUIAnimationTimer
{
    private static readonly Guid CLSID_UIAnimationTimer = new("BFCD4A0C-06B6-4384-B768-0DAA792C380E");

    /// <summary>
    /// Create a new instance of the <see cref="IUIAnimationTimer"/> class.
    /// </summary>
    public IUIAnimationTimer()
    {
        ComUtilities.CreateComInstance(
            CLSID_UIAnimationTimer,
            ComContext.InprocServer,
            typeof(IUIAnimationTimer).GUID,
            this);
    }

    /// <summary>	
    /// Determines whether the timer is currently enabled.
    /// </summary>	
    public bool IsEnabled => IsEnabled_().Success;
}
