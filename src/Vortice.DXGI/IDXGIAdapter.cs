// Copyright © Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using System.Collections.ObjectModel;

namespace Vortice.DXGI;

public partial class IDXGIAdapter
{
    private List<IDXGIOutput>? _outputs;

    protected override void NativePointerUpdated(IntPtr oldNativePointer)
    {
        base.NativePointerUpdated(oldNativePointer);
        if (oldNativePointer != IntPtr.Zero)
        {
            if (_outputs != null)
            {
                foreach (IDXGIOutput output in _outputs)
                {
                    MemoryHelpers.Dispose(output, true);
                }

                _outputs.Clear();
                _outputs = default;
            }
        }
    }

    public IEnumerable<IDXGIOutput> EnumOutputs()
    {
        if (_outputs == null)
        {
            _outputs = new List<IDXGIOutput>();
            while (true)
            {
                Result result = EnumOutputs(_outputs.Count, out IDXGIOutput? output);
                if (result.Failure || output == null)
                {
                    break;
                }

                _outputs.Add(output);
            }
        }

        return _outputs!;
    }

    /// <summary>
    /// Get an instance of <see cref="IDXGIAdapter"/> or null if not found.
    /// </summary>
    /// <remarks>
    /// Make sure to dispose the <see cref="IDXGIAdapter"/> instance.
    /// </remarks>
    /// <param name="index">The index to get from.</param>
    /// <returns>Instance of <see cref="IDXGIOutput"/> or null if not found.</returns>
    public IDXGIOutput GetOutput(int index)
    {
        EnumOutputs(index, out IDXGIOutput output).CheckError();
        return output;
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
