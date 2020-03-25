using SharpGen.Runtime;

namespace Vortice.Animation
{
    internal class IUIAnimationPriorityComparison2Callback : CallbackBase, IUIAnimationPriorityComparison2
    {
        public IUIAnimationManager2.PriorityComparisonDelegate Delegate;

        public Result HasPriority(IUIAnimationStoryboard2 scheduledStoryboard, IUIAnimationStoryboard2 newStoryboard, AnimationPriorityEffect priorityEffect)
        {
            if (Delegate != null)
                return Delegate.Invoke(scheduledStoryboard, newStoryboard, priorityEffect) ? Result.Ok : Result.False;
            return Result.False;
        }
    }
}
