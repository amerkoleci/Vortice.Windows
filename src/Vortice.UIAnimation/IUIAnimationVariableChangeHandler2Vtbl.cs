// Copyright (c) Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.UIAnimation;

internal static unsafe partial class IUIAnimationVariableChangeHandler2Vtbl
{
    private static unsafe partial int OnValueChangedImpl_(IntPtr thisObject, void* _storyboard, void* _variable, void* _newValue, void* _previousValue, int _cDimension)
    {
        IUIAnimationVariableChangeHandler2 @this = CppObjectShadow.ToCallback<IUIAnimationVariableChangeHandler2>(thisObject);

        try
        {
            Span<double> newValue = new double[_cDimension];
            Span<double> previousValue = new double[_cDimension];
            new Span<double>(_newValue, newValue.Length).CopyTo(newValue);
            new Span<double>(_previousValue, previousValue.Length).CopyTo(previousValue);
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
