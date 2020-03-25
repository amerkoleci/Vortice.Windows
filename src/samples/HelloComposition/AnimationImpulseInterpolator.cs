using System;
using System.Diagnostics;
using SharpGen.Runtime;
using Vortice.Animation;

namespace HelloComposition
{
    public sealed class AnimationImpulseInterpolator : CallbackBase, IUIAnimationInterpolator, IUIAnimationInterpolator2
    {
        private double _mDuration; // Duration of the transition
        private double _mInitialValue; // Initial value of the transition
        private double _mFinalValue; // Final value of the transition
        private double _mInitialVelocity; // Initial velocity of the transition
        private double _mAbsoluteAcceleration; // The original acceleration passed to the interpolator
        private double _mAcceleration; // Acceleration of the transition

        public Result SetInitialValueAndVelocity(double initialValue, double initialVelocity)
        {
            _mInitialValue = initialValue;
            _mInitialVelocity = initialVelocity;

            _mAcceleration = _mFinalValue > _mInitialValue ? _mAbsoluteAcceleration : -_mAbsoluteAcceleration;

            _mDuration =
                (-_mInitialVelocity + (_mAcceleration < 0.0 ? -1.0 : 1.0) *
                    Math.Sqrt(_mInitialVelocity * _mInitialVelocity +
                              2.0 * _mAcceleration * (_mFinalValue - _mInitialValue)))
                / _mAcceleration;

            return Result.Ok;
        }

        public Result GetDimension(out int dimension)
        {
            dimension = 1;
            
            return Result.Ok;
        }

        public Result SetInitialValueAndVelocity(double[] initialValue, double[] initialVelocity)
        {
            Debug.Assert(initialValue.Length == 1);
            Debug.Assert(initialVelocity.Length == 1);

            return SetInitialValueAndVelocity(initialValue[0], initialVelocity[0]);
        }

        public Result SetDuration(double duration)
        {
            return Result.Fail;
        }

        public Result GetDuration(out double duration)
        {
            duration = _mDuration;

            return Result.Ok;
        }

        public Result GetFinalValue(double[] value)
        {
            var result = GetFinalValue(out var value0);
            value[0] = value0;
            return result;
        }

        public Result InterpolateValue(double offset, double[] value)
        {
            var result = InterpolateValue(offset, out var value0);
            value[0] = value0;
            return result;
        }

        public Result InterpolateVelocity(double offset, double[] velocity)
        {
            var result = InterpolateVelocity(offset, out var velocity0);
            velocity[0] = velocity0;
            return result;
        }

        public Result GetPrimitiveInterpolation(IUIAnimationPrimitiveInterpolation interpolation, int cDimension)
        {
            return Result.Fail;
        }

        public Result GetFinalValue(out double value)
        {
            value = _mFinalValue;

            return Result.Ok;
        }

        public Result InterpolateValue(double offset, out double value)
        {
            value = 0.5 * _mAcceleration * (offset * offset) + _mInitialVelocity * offset + _mInitialValue;

            return Result.Ok;
        }

        public Result InterpolateVelocity(double offset, out double velocity)
        {
            velocity = _mAcceleration * offset + _mInitialVelocity;

            return Result.Ok;
        }

        public Result GetDependencies(out AnimationDependencies initialValueDependencies,
            out AnimationDependencies initialVelocityDependencies, out AnimationDependencies durationDependencies)
        {
            // The final value of the interpolator is not affected by the initial value or velocity, but
            // the intermediate values, final velocity and duration all are affected

            initialValueDependencies =
                AnimationDependencies.IntermediateValues |
                AnimationDependencies.FinalVelocity |
                AnimationDependencies.Duration;

            initialVelocityDependencies =
                AnimationDependencies.IntermediateValues |
                AnimationDependencies.FinalVelocity |
                AnimationDependencies.Duration;

            // This interpolator does not have a duration parameter, so SetDuration should not be called on it

            durationDependencies = AnimationDependencies.None;

            return Result.Ok;
        }

        private static AnimationImpulseInterpolator CreateInterpolator(double finalValue, double acceleration)
        {
            if (Math.Abs(acceleration) < 10 * double.Epsilon)
                throw new ArgumentOutOfRangeException(nameof(acceleration), "Acceleration is too small");

            return new AnimationImpulseInterpolator
            {
                _mAcceleration = acceleration,
                _mFinalValue = finalValue
            };
        }

        /// <summary>
        /// Creates an impulse transition
        /// </summary>
        /// <param name="factory">The transition factory to use to wrap the interpolator</param>
        /// <param name="finalValue">The final value this transition leads to when applied to an animation variable</param>
        /// <param name="acceleration">The rate of change of the velocity</param>
        /// <returns>The new impulse transition</returns>
        /// <exception cref="ArgumentOutOfRangeException">When acceleration is too small by absolute value</exception>
        public static IUIAnimationTransition CreateTransition(IUIAnimationTransitionFactory factory, double finalValue,
            double acceleration)
        {
            return factory.CreateTransition(CreateInterpolator(finalValue, acceleration));
        }

        /// <summary>
        /// Creates an impulse transition
        /// </summary>
        /// <param name="factory">The transition factory to use to wrap the interpolator</param>
        /// <param name="finalValue">The final value this transition leads to when applied to an animation variable</param>
        /// <param name="acceleration">The rate of change of the velocity</param>
        /// <returns>The new impulse transition</returns>
        /// <exception cref="ArgumentOutOfRangeException">When acceleration is too small by absolute value</exception>
        public static IUIAnimationTransition2 CreateTransition(IUIAnimationTransitionFactory2 factory, double finalValue,
            double acceleration)
        {
            return factory.CreateTransition(CreateInterpolator(finalValue, acceleration));
        }
    }
}
