// Copyright (c) Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.UIAnimation;

public partial class IUIAnimationTransition2
{
    /// <summary>	
    /// Determines whether the duration of a transition is known.
    /// </summary>	
    public bool IsDurationKnown => IsDurationKnown_().Success; 
}
