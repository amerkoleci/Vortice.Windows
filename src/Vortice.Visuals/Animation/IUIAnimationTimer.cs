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

using System;
using SharpGen.Runtime;

namespace Vortice.Animation
{
    public partial class IUIAnimationTimer
    {
        /// <summary>
        /// <p> Determines whether the timer is currently enabled.</p>
        /// </summary>
        /// <returns>true if the timer is enabled</returns>
        public bool IsEnabled => IsEnabled_().Success;

        private static readonly Guid TimerGuid = new Guid("BFCD4A0C-06B6-4384-B768-0DAA792C380E");

        /// <summary>
        /// Initializes a new instance of the <see cref="IUIAnimationTimer"/> class.
        /// </summary>
        public IUIAnimationTimer()
        {
            ComUtilities.CreateComInstance(TimerGuid, ComContext.InprocServer, typeof(IUIAnimationTimer).GUID, this);
        }
    }
}
