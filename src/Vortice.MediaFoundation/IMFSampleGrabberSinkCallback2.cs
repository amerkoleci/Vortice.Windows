// Copyright (c) Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using System.Runtime.CompilerServices;
using SharpGen.Runtime;
using static Vortice.MediaFoundation.MediaFactory;

namespace Vortice.MediaFoundation;

partial interface IMFSampleGrabberSinkCallback2
{
    void OnProcessSampleEx(Guid guidMajorMediaType, int sampleFlags, long sampleTime, long sampleDuration, Span<byte> sampleBuffer, IMFAttributes? attributes);
}

internal static unsafe partial class IMFSampleGrabberSinkCallback2Vtbl
{
    private static unsafe partial int OnProcessSampleExImpl_(nint thisObject,
        void* _guidMajorMediaType,
        int sampleFlags,
        long sampleTime, long sampleDuration,
        void* sampleBuffer,
        int sampleSize,
        void* _attributes)
    {
        IMFSampleGrabberSinkCallback2 @this = CppObjectShadow.ToCallback<IMFSampleGrabberSinkCallback2>(thisObject);
        try
        {
            Guid guidMajorMediaType = Unsafe.AsRef<Guid>(_guidMajorMediaType);
            @this.OnProcessSampleEx(guidMajorMediaType,
                sampleFlags,
                sampleTime,
                sampleDuration,
                new Span<byte>(sampleBuffer, sampleSize),
                _attributes != null ? new IMFAttributes((nint)_attributes) : default
                );
            return Result.Ok.Code;
        }
        catch (Exception exception)
        {
            (@this as IExceptionCallback)?.RaiseException(exception);
            return Result.GetResultFromException(exception).Code;
        }
    }
}
