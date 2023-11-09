// Copyright (c) Amer Koleci and contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.Direct3D12.Debug;

public partial class ID3D12InfoQueue1
{
    public delegate void MessageCallback(MessageCategory category, MessageSeverity severity, MessageId id, string description);

    private static MessageCallback? _messageCallback;
    private int _callbackCookie;

    protected override void DisposeCore(IntPtr nativePointer, bool disposing)
    {
        if (_callbackCookie != 0)
        {
            UnregisterMessageCallback(_callbackCookie);
            _callbackCookie = 0;
        }

        base.DisposeCore(nativePointer, disposing);
    }

    public void RegisterMessageCallback(MessageCallback? callback, MessageCallbackFlags callbackFilterFlags = MessageCallbackFlags.None)
    {
        _messageCallback = callback;

        unsafe
        {
            if (_callbackCookie == 0)
            {
                nint context = 0;
                RegisterMessageCallback(callback != null ? new(_nativeMessageCallback) : null, callbackFilterFlags, ref context, out _callbackCookie);
            }
        }
    }

    #region Native to managed callback
    private static unsafe delegate*<MessageCategory, MessageSeverity, MessageId, sbyte*, void*, void> _nativeMessageCallback = &OnNativeMessageCallback;

    private static unsafe void OnNativeMessageCallback(MessageCategory category, MessageSeverity severity, MessageId id, sbyte* pDescription, void* context)
    {
        string description = new(pDescription);

        if (_messageCallback != null)
        {
            _messageCallback(category, severity, id, description);
        }
    }
    #endregion
}
