// Copyright © Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.UIAnimation;

public partial interface IUIAnimationVariableIntegerChangeHandler2
{
    void OnValueChanged(IUIAnimationStoryboard2 storyboard, IUIAnimationVariable2 variable, Span<int> newValue, Span<int> previousValue, int dimension);
}
