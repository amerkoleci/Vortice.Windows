// Copyright (c) Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using SharpGen.Runtime;
using SharpGen.Runtime.Win32;

namespace Vortice.MediaFoundation;

public abstract class IMFSourceReaderCallbackBase : CallbackBase, IMFSourceReaderCallback
{
    public virtual void OnEvent(SourceReaderIndex streamIndex, IMFMediaEvent @event)
    {

    }

    public virtual void OnFlush(SourceReaderIndex streamIndex)
    {

    }

    public virtual void OnReadSample(Result hrStatus,
        SourceReaderIndex streamIndex,
        SourceReaderFlag streamFlags, long llTimestamp, IMFSample sample)
    {

    }

    #region IMFSourceReaderCallback Members
    void IMFSourceReaderCallback.OnEvent(int streamIndex, IMFMediaEvent @event)
    {
        OnEvent((SourceReaderIndex)streamIndex, @event);
    }

    void IMFSourceReaderCallback.OnFlush(int streamIndex)
    {
        OnFlush((SourceReaderIndex)streamIndex);
    }

    void IMFSourceReaderCallback.OnReadSample(Result hrStatus, int streamIndex, int streamFlags, long timestamp, IMFSample sample)
    {
        OnReadSample(hrStatus, (SourceReaderIndex)streamIndex, (SourceReaderFlag)streamFlags, timestamp, sample);
    }
    #endregion 
}
