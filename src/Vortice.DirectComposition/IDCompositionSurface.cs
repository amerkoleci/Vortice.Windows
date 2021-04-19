// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using System.Drawing;
using SharpGen.Runtime;

namespace Vortice.DirectComposition
{
    public partial class IDCompositionSurface
    {
        public T BeginDraw<T>(RawRect? updateRect, out Point updateOffset) where T : ComObject
        {
            BeginDraw(updateRect, typeof(T).GUID, out IntPtr updateObjectPtr, out updateOffset).CheckError();
            return FromPointer<T>(updateObjectPtr);
        }

        public Result BeginDraw<T>(RawRect? updateRect, out T updateObject, out Point updateOffset) where T : ComObject
        {
            Result result = BeginDraw(updateRect, typeof(T).GUID, out IntPtr updateObjectPtr, out updateOffset);
            if (result.Failure)
            {
                updateObject = default;
                return result;
            }

            updateObject = FromPointer<T>(updateObjectPtr);
            return result;
        }
    }
}
