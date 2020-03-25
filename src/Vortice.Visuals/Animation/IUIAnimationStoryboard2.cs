using System;
using System.Runtime.InteropServices;
using SharpGen.Runtime;

namespace Vortice.Animation
{
    public partial class IUIAnimationStoryboard2
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IUIAnimationStoryboard2"/> class.
        /// </summary>
        /// <param name="manager">The manager.</param>
        public IUIAnimationStoryboard2(IUIAnimationManager2 manager)
        {
            manager.CreateStoryboard(this);
        }
        
        /// <summary>
        /// Sets the tag.
        /// </summary>
        /// <param name="object">The @object.</param>
        /// <param name="id">The id.</param>
        /// <returns>A <see cref="Result" /> object describing the result of the operation.</returns>
        /// <unmanaged>HRESULT IUIAnimationStoryboard::SetTag([In, Optional] void* object,[In] unsigned int id)</unmanaged>
        public void SetTag(object @object, int id)
        {
            // Free any previous tag set
            IntPtr tagObjectPtr = IntPtr.Zero;
            int previousId;
            GetTag(out tagObjectPtr, out previousId);
            if (tagObjectPtr != IntPtr.Zero)
                GCHandle.FromIntPtr(tagObjectPtr).Free();

            SetTag(GCHandle.ToIntPtr(GCHandle.Alloc(@object)), id);
        }

        /// <summary>
        /// Gets the tag.
        /// </summary>
        /// <param name="object">The @object.</param>
        /// <param name="id">The id.</param>
        /// <returns>
        /// A <see cref="Result"/> object describing the result of the operation.
        /// </returns>
        /// <unmanaged>HRESULT IUIAnimationStoryboard::GetTag([Out, Optional] void** object,[Out, Optional] unsigned int* id)</unmanaged>
        public void GetTag(out object @object, out int id)
        {
            IntPtr tagObjectPtr;
            GetTag(out tagObjectPtr, out id);
            @object = GCHandle.FromIntPtr(tagObjectPtr).Target;
        }
    }
}
