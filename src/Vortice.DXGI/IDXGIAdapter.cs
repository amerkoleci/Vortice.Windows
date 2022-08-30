// Copyright © Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.DXGI;

public partial class IDXGIAdapter
{
    private readonly List<IDXGIOutput> _outputs = new(8);

    protected override void NativePointerUpdated(IntPtr oldNativePointer)
    {
        base.NativePointerUpdated(oldNativePointer);
        if (oldNativePointer != IntPtr.Zero)
        {
            DisposeOutputs();
        }
    }

    private void DisposeOutputs()
    {
        if (_outputs.Count > 0)
        {
            foreach (IDXGIOutput output in _outputs)
            {
                MemoryHelpers.Dispose(output, true);
            }

            _outputs.Clear();
        }
    }

    public IEnumerable<IDXGIOutput> EnumOutputs()
    {
        DisposeOutputs();

        while (true)
        {
            Result result = EnumOutputs(_outputs.Count, out IDXGIOutput? output);
            if (result.Failure || output == null)
            {
                break;
            }

            _outputs.Add(output);
        }

        return _outputs;
    }

    public bool CheckInterfaceSupport<T>() where T : ComObject
    {
        return CheckInterfaceSupport(typeof(T), out _);
    }

    public bool CheckInterfaceSupport<T>(out long userModeVersion) where T : ComObject
    {
        return CheckInterfaceSupport(typeof(T), out userModeVersion);
    }

    public bool CheckInterfaceSupport(Type type, out long userModeDriverVersion)
    {
        return CheckInterfaceSupport(type.GUID, out userModeDriverVersion).Success;
    }
}
