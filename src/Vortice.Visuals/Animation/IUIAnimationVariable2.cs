using System;
using System.Runtime.InteropServices;
using SharpGen.Runtime;

namespace Vortice.Animation
{
    public partial class IUIAnimationVariable2
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IUIAnimationVariable2"/> class.
        /// </summary>
        /// <param name="manager">The manager.</param>
        /// <param name="initialValue">The initial value.</param>
        public IUIAnimationVariable2(IUIAnimationManager2 manager, double initialValue = 0.0)
        {
            manager.CreateAnimationVariable(initialValue, this);
        }

        /// <summary>
        /// Sets the tag.
        /// </summary>
        /// <param name="object">The @object.</param>
        /// <param name="id">The id.</param>
        /// <returns>A <see cref="Result" /> object describing the result of the operation.</returns>
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
        public void GetTag(out object @object, out int id)
        {
            IntPtr tagObjectPtr;
            GetTag(out tagObjectPtr, out id);
            @object = GCHandle.FromIntPtr(tagObjectPtr).Target;
        }
    }
}
