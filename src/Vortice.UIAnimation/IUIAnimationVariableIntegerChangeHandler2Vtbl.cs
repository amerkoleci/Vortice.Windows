// Copyright © Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.UIAnimation;

internal static unsafe partial class IUIAnimationVariableIntegerChangeHandler2Vtbl
{
    private static unsafe partial int OnIntegerValueChangedImpl_(IntPtr thisObject, void* _storyboard, void* _variable, void* _newValue, void* _previousValue, int _cDimension)
    {
        IUIAnimationVariableIntegerChangeHandler2 @this = CppObjectShadow.ToCallback<IUIAnimationVariableIntegerChangeHandler2>(thisObject);

        try
        {
            Span<int> newValue = new int[_cDimension];
            Span<int> previousValue = new int[_cDimension];
            new Span<int>(_newValue, newValue.Length).CopyTo(newValue);
            new Span<int>(_previousValue, previousValue.Length).CopyTo(previousValue);
            IUIAnimationStoryboard2 storyboard = new((IntPtr)_storyboard);
            IUIAnimationVariable2 variable = new((IntPtr)_variable);
            @this.OnValueChanged(storyboard, variable, newValue, previousValue, _cDimension);
            return Result.Ok.Code;
        }
        catch (Exception __exception__)
        {
            (@this as IExceptionCallback)?.RaiseException(__exception__);
            return Result.GetResultFromException(__exception__).Code;
        }
    }
}
