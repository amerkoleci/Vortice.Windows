// Copyright © Aaron Sun, Amer Koleci, and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.DirectML;

public partial struct BindingDescription
{
    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_BINDING_DESC::Desc']/*" />
    public IBindingDescription Description { get; set; }

    public BindingDescription(IBindingDescription binding)
    {
        Description = binding;
    }

    public override string ToString() => $"{Description} as {nameof(BindingDescription)}";

    #region Marshal
    [StructLayout(LayoutKind.Sequential, Pack = 0)]
    internal struct __Native
    {
        public BindingType Type;
        public IntPtr Description;
    }

    internal void __MarshalFree(ref __Native @ref)
    {
        if (Description != null)
        {
            ((IBindingDescriptionMarshal)Description).__MarshalFree(ref @ref.Description);
        }

        @ref.Description = IntPtr.Zero;
    }

    internal void __MarshalTo(ref __Native @ref)
    {
        // A default BindingDescription marshals as DML_BINDING_TYPE_NONE, which
        // is how an optional operator tensor that was left null is skipped in
        // BindInputs and BindOutputs.
        @ref.Type = Description?.BindingType ?? BindingType.None;
        @ref.Description = (Description != null) ? ((IBindingDescriptionMarshal)Description).__MarshalAlloc() : IntPtr.Zero;
    }
    #endregion
}
