// Copyright (c) 2010-2014 SharpDX - Alexandre Mutel
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
// Copyright (c) Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using System.Globalization;
using SharpGen.Runtime;
using Vortice.Win32;
using static Vortice.WinForms.Win32Native;

namespace Vortice.WinForms;

/// <summary>
/// RenderLoop provides a rendering loop infrastructure. See remarks for usage. 
/// </summary>
/// <remarks>
/// Use static <see cref="Run(Control?, RenderCallback, bool)"/> 
/// method to directly use a renderloop with a render callback or use your own loop:
/// <code>
/// control.Show();
/// using (RenderLoop loop = new(control))
/// {
///     while (loop.NextFrame())
///     {
///        // Perform draw operations here.
///     }
/// }
/// </code>
/// Note that the main control can be changed at anytime inside the loop.
/// </remarks>
public class RenderLoop : IDisposable
{
    private nint _controlHandle;
    private Control? _control;
    private bool _isControlAlive;
    private bool _switchControl;

    /// <summary>
    /// Initializes a new instance of the <see cref="RenderLoop"/> class.
    /// </summary>
    public RenderLoop()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="RenderLoop"/> class.
    /// </summary>
    public RenderLoop(Control control)
    {
        Control = control;
    }

    /// <summary>
    /// Gets or sets the control to associate with the current render loop.
    /// </summary>
    /// <value>The control.</value>
    /// <exception cref="InvalidOperationException">Control is already disposed</exception>
    public Control? Control
    {
        get => _control;
        set
        {
            if (_control == value)
                return;

            // Remove any previous control
            if (_control != null && !_switchControl)
            {
                _isControlAlive = false;
                _control.Disposed -= ControlDisposed;
                _controlHandle = 0;
            }

            if (value != null && value.IsDisposed)
            {
                throw new InvalidOperationException("Control is already disposed");
            }

            _control = value;
            _switchControl = true;
        }
    }

    /// <summary>
    /// Gets or sets a value indicating whether the render loop should use the default <see cref="Application.DoEvents"/> instead of a custom window message loop lightweight for GC. Default is false.
    /// </summary>
    /// <value><c>true</c> if the render loop should use the default <see cref="Application.DoEvents"/> instead of a custom window message loop (default false); otherwise, <c>false</c>.</value>
    /// <remarks>By default, RenderLoop is using a custom window message loop that is more lightweight than <see cref="Application.DoEvents" /> to process windows event message. 
    /// Set this parameter to true to use the default <see cref="Application.DoEvents"/>.</remarks>
    public bool UseApplicationDoEvents { get; set; }

    /// <summary>
    /// Calls this method on each frame.
    /// </summary>
    /// <returns><c>true</c> if if the control is still active, <c>false</c> otherwise.</returns>
    /// <exception cref="InvalidOperationException">An error occured </exception>
    public unsafe bool NextFrame()
    {
        // Setup new control
        // TODO this is not completely thread-safe. We should use a lock to handle this correctly
        if (_switchControl && _control != null)
        {
            _controlHandle = _control.Handle;
            _control.Disposed += ControlDisposed;
            _isControlAlive = true;
            _switchControl = false;
        }

        if (_isControlAlive)
        {
            if (UseApplicationDoEvents)
            {
                // Revert back to Application.DoEvents in order to support Application.AddMessageFilter
                // Seems that DoEvents is compatible with Mono unlike Application.Run that was not running
                // correctly.
                Application.DoEvents();
            }
            else
            {
                nint localHandle = _controlHandle;
                if (localHandle != 0)
                {
                    // Previous code not compatible with Application.AddMessageFilter but faster then DoEvents
                    NativeMessage msg;
                    while (PeekMessageW(&msg, 0, 0, 0, 0))
                    {
                        if (GetMessageW(&msg, 0, 0, 0) == new RawBool(-1))
                        {
                            throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture,
                                "An error happened in rendering loop while processing windows messages. Error: {0}",
                                Marshal.GetLastWin32Error()));
                        }

                        // NCDESTROY event?
                        if (msg.msg == 130)
                        {
                            _isControlAlive = false;
                        }

                        var message = new Message() { HWnd = msg.hwnd, LParam = msg.lParam, Msg = (int)msg.msg, WParam = (nint)(msg.wParam) };
                        if (!Application.FilterMessage(ref message))
                        {
                            _ = TranslateMessage(&msg);
                            _ = DispatchMessageW(&msg);
                        }
                    }
                }
            }
        }

        return _isControlAlive || _switchControl;
    }

    private void ControlDisposed(object? sender, EventArgs e)
    {
        _isControlAlive = false;
    }

    /// <summary>
    /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
    /// </summary>
    public void Dispose()
    {
        Control = null;
        GC.SuppressFinalize(this);
    }

    /// <summary>
    /// Delegate for the rendering loop.
    /// </summary>
    public delegate void RenderCallback();

    /// <summary>
    /// Runs the specified main loop in the specified context.
    /// </summary>
    public static void Run(ApplicationContext context, RenderCallback renderCallback)
    {
        Run(context.MainForm, renderCallback);
    }

    /// <summary>
    /// Runs the specified main loop for the specified windows form.
    /// </summary>
    /// <param name="form">The form.</param>
    /// <param name="renderCallback">The rendering callback.</param>
    /// <param name="useApplicationDoEvents">if set to <c>true</c> indicating whether the render loop should use the default <see cref="Application.DoEvents"/> instead of a custom window message loop lightweight for GC. Default is false.</param>
    /// <exception cref="ArgumentNullException">form
    /// or
    /// renderCallback</exception>
    public static void Run(Control? form, RenderCallback renderCallback, bool useApplicationDoEvents = false)
    {
        if (form == null)
        {
            throw new ArgumentNullException("form");
        }

        ArgumentNullException.ThrowIfNull(renderCallback);

        form.Show();
        using (var renderLoop = new RenderLoop(form) { UseApplicationDoEvents = useApplicationDoEvents })
        {
            while (renderLoop.NextFrame())
            {
                renderCallback();
            }
        }
    }

    /// <summary>
    /// Gets a value indicating whether this instance is application idle.
    /// </summary>
    /// <value>
    /// 	<c>true</c> if this instance is application idle; otherwise, <c>false</c>.
    /// </value>
    public static unsafe bool IsIdle
    {
        get
        {
            NativeMessage msg;
            return PeekMessageW(&msg, 0, 0, 0, 0) == new RawBool(0);
        }
    }
}
