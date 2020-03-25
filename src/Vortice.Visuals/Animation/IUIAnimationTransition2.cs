namespace Vortice.Animation
{
    public partial class IUIAnimationTransition2
    {
        /// <summary>
        /// <p>Determines whether a transition's duration is currently known.</p>
        /// </summary>
        /// <returns>True if the duration is known</returns>
        public bool IsDurationKnown => IsDurationKnown_().Success;
    }
}
