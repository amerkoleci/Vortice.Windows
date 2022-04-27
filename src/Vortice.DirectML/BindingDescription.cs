// Copyright © Aaron Sun, Amer Koleci, and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.DirectML;

public partial class BindingDescription
{
    public IBinding Binding { get; set; }

    public BindingDescription(IBinding binding)
    {
        Binding = binding;
    }

    #region Marshal
    [StructLayout(LayoutKind.Sequential, Pack = 0)]
    internal struct __Native
    {
        public BindingType Type;
        public IntPtr Description;
    }

    internal void __MarshalFree(ref __Native @ref)
    {
        ((IBindingMarshal)Binding).__MarshalFree(ref @ref.Description);
        @ref.Description = IntPtr.Zero;
    }

    internal void __MarshalTo(ref __Native @ref)
    {
        @ref.Type = Binding.BindingType;
        @ref.Description = ((IBindingMarshal)Binding).__MarshalAlloc();
    }
    #endregion
}
