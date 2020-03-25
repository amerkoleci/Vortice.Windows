using SharpGen.Runtime;

namespace Vortice.Animation
{
    internal class IUIAnimationManagerEventHandler2Callback : CallbackBase, IUIAnimationManagerEventHandler2
    {
        public IUIAnimationManager2.StatusChangedDelegate Delegates;

        public void OnManagerStatusChanged(AnimationManagerStatus newStatus, AnimationManagerStatus previousStatus)
        {
            Delegates?.Invoke(newStatus, previousStatus);
        }
    }
}
