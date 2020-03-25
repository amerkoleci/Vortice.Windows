// Copyright (c) 2010-2014 SharpDX - Alexandre Mutel
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.

using System;
using System.Runtime.InteropServices;
using SharpGen.Runtime;

namespace Vortice.Animation
{
    public partial class IUIAnimationManager
    {
        private static readonly Guid ManagerGuid = new Guid("4C1FC63A-695C-47E8-A339-1A194BE3D0B8");

        /// <summary>
        /// Initializes a new instance of the <see cref="IUIAnimationManager"/> class.
        /// </summary>
        public IUIAnimationManager()
        {
            ComUtilities.CreateComInstance(ManagerGuid, ComContext.InprocServer, typeof(IUIAnimationManager).GUID, this);
        }
        
        private IUIAnimationManagerEventHandlerCallback _statusEventHandler;

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
        public delegate bool PriorityComparisonDelegate(IUIAnimationStoryboard scheduledStoryboard, IUIAnimationStoryboard newStoryboard, AnimationPriorityEffect priorityEffect);

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
                    _statusEventHandler = new IUIAnimationManagerEventHandlerCallback();
                    SetManagerEventHandler(_statusEventHandler);
                }
                _statusEventHandler.Delegates += value;
            }

            remove
            {
                if (_statusEventHandler == null) return;

                _statusEventHandler.Delegates -= value;

                if (_statusEventHandler.Delegates.GetInvocationList().Length == 0)
                {
                    SetManagerEventHandler(null);
                    _statusEventHandler.Dispose();
                    _statusEventHandler = null;
                }
            }
        }

        private IUIAnimationPriorityComparisonCallback _cancelPriorityComparisonCallback;

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
                        _cancelPriorityComparisonCallback = new IUIAnimationPriorityComparisonCallback { Delegate = value };
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

        private IUIAnimationPriorityComparisonCallback _trimPriorityComparisonCallback;

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
                        _trimPriorityComparisonCallback = new IUIAnimationPriorityComparisonCallback { Delegate = value };
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

        private IUIAnimationPriorityComparisonCallback _compressPriorityComparisonCallback;

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
                        _compressPriorityComparisonCallback = new IUIAnimationPriorityComparisonCallback { Delegate = value };
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

        private IUIAnimationPriorityComparisonCallback _concludePriorityComparisonCallback;

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
                        _concludePriorityComparisonCallback = new IUIAnimationPriorityComparisonCallback { Delegate = value };
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
        public IUIAnimationVariable GetVariableFromTag(int id, object tagObject = null)
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
        public IUIAnimationStoryboard GetStoryboardFromTag(int id, object tagObject = null)
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
                SetManagerEventHandler(null);
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
