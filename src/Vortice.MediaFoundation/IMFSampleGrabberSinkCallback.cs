// Copyright (c) Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using System.Runtime.CompilerServices;
using SharpGen.Runtime;
using static Vortice.MediaFoundation.MediaFactory;

namespace Vortice.MediaFoundation;

partial interface IMFSampleGrabberSinkCallback
{
    void OnSetPresentationClock(IMFPresentationClock presentationClock);
    void OnProcessSample(Guid guidMajorMediaType, int sampleFlags, long sampleTime, long sampleDuration, Span<byte> sampleBuffer);
    void OnShutdown();
}

internal static unsafe partial class IMFSampleGrabberSinkCallbackVtbl
{
    private static unsafe partial int OnSetPresentationClockImpl_(nint thisObject, void* _presentationClock)
    {
        IMFSampleGrabberSinkCallback @this = CppObjectShadow.ToCallback<IMFSampleGrabberSinkCallback>(thisObject);
        try
        {
            IMFPresentationClock presentationClock = new((nint)_presentationClock);
            @this.OnSetPresentationClock(presentationClock);
            return Result.Ok.Code;
        }
        catch (Exception exception)
        {
            (@this as IExceptionCallback)?.RaiseException(exception);
            return Result.GetResultFromException(exception).Code;
        }
    }

    private static unsafe partial int OnProcessSampleImpl_(nint thisObject,
        void* _guidMajorMediaType,
        int sampleFlags,
        long sampleTime, long sampleDuration, void* sampleBuffer, int sampleSize)
    {
        IMFSampleGrabberSinkCallback @this = CppObjectShadow.ToCallback<IMFSampleGrabberSinkCallback>(thisObject);
        try
        {
            Guid guidMajorMediaType = Unsafe.AsRef<Guid>(_guidMajorMediaType);

            @this.OnProcessSample(
                guidMajorMediaType,
                sampleFlags,
                sampleTime,
                sampleDuration,
                new Span<byte>(sampleBuffer, sampleSize)
                );
            return Result.Ok.Code;
        }
        catch (Exception exception)
        {
            (@this as IExceptionCallback)?.RaiseException(exception);
            return Result.GetResultFromException(exception).Code;
        }
    }

    private static unsafe partial int OnShutdownImpl_(nint thisObject)
    {
        IMFSampleGrabberSinkCallback @this = CppObjectShadow.ToCallback<IMFSampleGrabberSinkCallback>(thisObject);
        try
        {
            @this.OnShutdown();
            return Result.Ok.Code;
        }
        catch (Exception exception)
        {
            (@this as IExceptionCallback)?.RaiseException(exception);
            return Result.GetResultFromException(exception).Code;
        }
    }
}
