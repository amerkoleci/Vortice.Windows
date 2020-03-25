using System;
using SharpGen.Runtime;

namespace Vortice.Animation
{
    public partial class IUIAnimationTransitionLibrary2
    {
        private static readonly Guid TransitionLibraryGuid = new Guid("812F944A-C5C8-4CD9-B0A6-B3DA802F228D");

        /// <summary>
        /// Initializes a new instance of the <see cref="IUIAnimationTransitionLibrary2"/> class.
        /// </summary>
        public IUIAnimationTransitionLibrary2()
        {
            ComUtilities.CreateComInstance(TransitionLibraryGuid, ComContext.InprocServer,
                typeof(IUIAnimationTransitionLibrary2).GUID, this);
        }
    }
}
