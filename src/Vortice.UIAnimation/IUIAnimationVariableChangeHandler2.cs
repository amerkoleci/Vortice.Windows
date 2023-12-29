// Copyright (c) Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.UIAnimation;

public partial interface IUIAnimationVariableChangeHandler2
{
    void OnValueChanged(IUIAnimationStoryboard2 storyboard, IUIAnimationVariable2 variable, Span<double> newValue, Span<double> previousValue, int dimension);
}
