using System;
using SharpGen.Runtime;

namespace Vortice.Animation
{
    public partial class IUIAnimationTransitionFactory2
    {
        private static readonly Guid TransitionFactoryGuid = new Guid("84302F97-7F7B-4040-B190-72AC9D18E420");

        /// <summary>
        /// Initializes a new instance of the <see cref="IUIAnimationTransitionFactory2"/> class.
        /// </summary>
        public IUIAnimationTransitionFactory2()
        {
            ComUtilities.CreateComInstance(TransitionFactoryGuid, ComContext.InprocServer,
                typeof(IUIAnimationTransitionFactory2).GUID, this);
        }
    }
}
