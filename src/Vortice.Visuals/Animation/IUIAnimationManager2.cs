using System;
using System.Runtime.InteropServices;
using SharpGen.Runtime;

namespace Vortice.Animation
{
    public partial class IUIAnimationManager2
    {
        private static readonly Guid ManagerGuid = new Guid("D25D8842-8884-4A4A-B321-091314379BDD");

        /// <summary>
        /// Initializes a new instance of the <see cref="IUIAnimationManager2"/> class.
        /// </summary>
        public IUIAnimationManager2()
        {
            ComUtilities.CreateComInstance(ManagerGuid, ComContext.InprocServer, typeof(IUIAnimationManager2).GUID,
                this);
        }
        
        private IUIAnimationManagerEventHandler2Callback _statusEventHandler;

        /// <summary>
        /// A delegate to receive status changed events from the manager.
        /// </summary>
        /// <param name="newStatus">The new status.</param>
        /// <param name="previousStatus">The previous status.</param>
        public delegate void StatusChangedDelegate(AnimationManagerStatus newStatus, AnimationManagerStatus previousStatus);

        /// <summary>
        /// A delegate used to resolve scheduling conflicts.
        /// </summary>
        /// <param name="scheduledStoryboard">The scheduled storyboard.</param>
        /// <param name="newStoryboard">The new storyboard.</param>
        /// <param name="priorityEffect">The priority effect.</param>
        /// <returns><c>true</c> if newStoryboard has priority. <c>false</c> if scheduledStoryboard has priority</returns>
        public delegate bool PriorityComparisonDelegate(IUIAnimationStoryboard2 scheduledStoryboard, IUIAnimationStoryboard2 newStoryboard, AnimationPriorityEffect priorityEffect);

        /// <summary>
        /// Occurs when [status changed].
        /// </summary>
        public event StatusChangedDelegate StatusChanged
        {
            add
            {
                // Setup the Manager Event Handler delegates
                if (_statusEventHandler == null)
                {
                    _statusEventHandler = new IUIAnimationManagerEventHandler2Callback();
                    SetManagerEventHandler(_statusEventHandler, true);
                }
                _statusEventHandler.Delegates += value;
            }

            remove
            {
                if (_statusEventHandler == null) return;

                _statusEventHandler.Delegates -= value;

                if (_statusEventHandler.Delegates.GetInvocationList().Length == 0)
                {
                    SetManagerEventHandler(null, false);
                    _statusEventHandler.Dispose();
                    _statusEventHandler = null;
                }
            }
        }

        private IUIAnimationPriorityComparison2Callback _cancelPriorityComparisonCallback;

        /// <summary>
        /// Sets the cancel priority comparison.
        /// </summary>
        /// <value>
        /// The cancel priority comparison.
        /// </value>
        public PriorityComparisonDelegate CancelPriorityComparison
        {
            set
            {
                if (value != null)
                {
                    if (_cancelPriorityComparisonCallback == null)
                    {
                        _cancelPriorityComparisonCallback = new IUIAnimationPriorityComparison2Callback { Delegate = value };
                        SetCancelPriorityComparison(_cancelPriorityComparisonCallback);
                    }
                    _cancelPriorityComparisonCallback.Delegate = value;
                }
                else if (_cancelPriorityComparisonCallback != null)
                {
                    SetCancelPriorityComparison(null);
                    _cancelPriorityComparisonCallback.Dispose();
                    _cancelPriorityComparisonCallback = null;
                }
            }
        }

        private IUIAnimationPriorityComparison2Callback _trimPriorityComparisonCallback;

        /// <summary>
        /// Sets the trim priority comparison.
        /// </summary>
        /// <value>
        /// The trim priority comparison.
        /// </value>
        public PriorityComparisonDelegate TrimPriorityComparison
        {
            set
            {
                if (value != null)
                {
                    if (_trimPriorityComparisonCallback == null)
                    {
                        _trimPriorityComparisonCallback = new IUIAnimationPriorityComparison2Callback { Delegate = value };
                        SetTrimPriorityComparison(_trimPriorityComparisonCallback);
                    }
                    _trimPriorityComparisonCallback.Delegate = value;
                }
                else if (_trimPriorityComparisonCallback != null)
                {
                    SetTrimPriorityComparison(null);
                    _trimPriorityComparisonCallback.Dispose();
                    _trimPriorityComparisonCallback = null;
                }
            }
        }

        private IUIAnimationPriorityComparison2Callback _compressPriorityComparisonCallback;

        /// <summary>
        /// Sets the compress priority comparison.
        /// </summary>
        /// <value>
        /// The compress priority comparison.
        /// </value>
        public PriorityComparisonDelegate CompressPriorityComparison
        {
            set
            {
                if (value != null)
                {
                    if (_compressPriorityComparisonCallback == null)
                    {
                        _compressPriorityComparisonCallback = new IUIAnimationPriorityComparison2Callback { Delegate = value };
                        SetCompressPriorityComparison(_compressPriorityComparisonCallback);
                    }
                    _compressPriorityComparisonCallback.Delegate = value;
                }
                else if (_compressPriorityComparisonCallback != null)
                {
                    SetCompressPriorityComparison(null);
                    _compressPriorityComparisonCallback.Dispose();
                    _compressPriorityComparisonCallback = null;
                }
            }
        }

        private IUIAnimationPriorityComparison2Callback _concludePriorityComparisonCallback;

        /// <summary>
        /// Sets the conclude priority comparison.
        /// </summary>
        /// <value>
        /// The conclude priority comparison.
        /// </value>
        public PriorityComparisonDelegate ConcludePriorityComparison
        {
            set
            {
                if (value != null)
                {
                    if (_concludePriorityComparisonCallback == null)
                    {
                        _concludePriorityComparisonCallback = new IUIAnimationPriorityComparison2Callback { Delegate = value };
                        SetConcludePriorityComparison(_concludePriorityComparisonCallback);
                    }
                    _concludePriorityComparisonCallback.Delegate = value;
                }
                else if (_concludePriorityComparisonCallback != null)
                {
                    SetConcludePriorityComparison(null);
                    _concludePriorityComparisonCallback.Dispose();
                    _concludePriorityComparisonCallback = null;
                }
            }
        }
        

        /// <summary>
        /// Gets the variable from tag.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="tagObject">The tag object. This parameter can be null.</param>
        /// <returns>A variable associated with this tag.</returns>
        /// <unmanaged>HRESULT IUIAnimationManager::GetVariableFromTag([In, Optional] void* object,[In] unsigned int id,[Out] IUIAnimationVariable** variable)</unmanaged>
        public IUIAnimationVariable2 GetVariableFromTag(int id, object tagObject = null)
        {
            var tagObjectHandle = GCHandle.Alloc(tagObject);
            try
            {
                return GetVariableFromTag(GCHandle.ToIntPtr(tagObjectHandle), id);
            }
            finally
            {
                tagObjectHandle.Free();
            }
        }

        /// <summary>
        /// Gets the storyboard from tag.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="tagObject">The tag object. This parameter can be null.</param>
        /// <returns>A storyboard associated with this tag.</returns>
        /// <unmanaged>HRESULT IUIAnimationManager::GetStoryboardFromTag([In, Optional] void* object,[In] unsigned int id,[Out] IUIAnimationStoryboard** storyboard)</unmanaged>
        public IUIAnimationStoryboard2 GetStoryboardFromTag(int id, object tagObject = null)
        {
            var tagObjectHandle = GCHandle.Alloc(tagObject);
            try
            {
                return GetStoryboardFromTag(GCHandle.ToIntPtr(tagObjectHandle), id);
            }
            finally
            {
                tagObjectHandle.Free();
            }
        }

        /// <inheritdoc/>
        protected override void Dispose(bool disposing)
        {
            if (_statusEventHandler != null)
            {
                SetManagerEventHandler(null, false);
                _statusEventHandler.Dispose();
                _statusEventHandler = null;
            }

            if (_cancelPriorityComparisonCallback != null)
            {
                SetCancelPriorityComparison(null);
                _cancelPriorityComparisonCallback.Dispose();
                _cancelPriorityComparisonCallback = null;
            }

            if (_concludePriorityComparisonCallback != null)
            {
                SetConcludePriorityComparison(null);
                _concludePriorityComparisonCallback.Dispose();
                _concludePriorityComparisonCallback = null;
            }

            if (_compressPriorityComparisonCallback != null)
            {
                SetCompressPriorityComparison(null);
                _compressPriorityComparisonCallback.Dispose();
                _compressPriorityComparisonCallback = null;
            }

            if (_concludePriorityComparisonCallback != null)
            {
                SetConcludePriorityComparison(null);
                _concludePriorityComparisonCallback.Dispose();
                _concludePriorityComparisonCallback = null;
            }

            base.Dispose(disposing);
        }
    }
}
