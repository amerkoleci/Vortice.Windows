using Vortice.Animation;

namespace HelloComposition
{
    public sealed partial class CompositionVisualPosition
    {
        public delegate void AnimationFactory(ref AnimationFactoryArgs args);

        public ref struct AnimationFactoryArgs
        {
            public readonly float PreviousTargetValue;
            public readonly float TargetValue;
            public IUIAnimationTransition2 Transition;

            internal AnimationFactoryArgs(float previousTargetValue, float targetValue)
            {
                PreviousTargetValue = previousTargetValue;
                TargetValue = targetValue;
                Transition = null;
            }
        }

        public event AnimationFactory AnimationFactories;

        private IUIAnimationTransition2 CreateAnimation(float previousTargetValue, float targetValue)
        {
            var args = new AnimationFactoryArgs(previousTargetValue, targetValue);

            AnimationFactories?.Invoke(ref args);

            if (args.Transition is null)
                DefaultAnimationFactory(ref args);

            return args.Transition;
        }

        private void DefaultAnimationFactory(ref AnimationFactoryArgs args)
        {
            args.Transition = _graphicsDevice.AnimationTransitionLibrary.AccelerateDecelerate(
                1,
                args.TargetValue,
                0.5,
                0.5
            );
        }
    }
}
