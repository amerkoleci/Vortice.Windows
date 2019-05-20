// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using System.Numerics;
using System.Runtime.InteropServices;
using SharpGen.Runtime;

namespace Vortice.DirectX.Direct2D
{
    internal class ID2D1GeometrySinkShadow : ID2D1SimplifiedGeometrySinkShadow
    {
        /// <summary>
        /// Get a native callback pointer from a managed callback.
        /// </summary>
        /// <param name="geometrySink">The geometry sink.</param>
        /// <returns>A pointer to the unmanaged geometry sink counterpart</returns>
        public static IntPtr ToIntPtr(ID2D1GeometrySink geometrySink)
        {
            return ToCallbackPtr<ID2D1GeometrySink>(geometrySink);
        }

        private unsafe class GeometrySinkVtbl : SimplifiedGeometrySinkVtbl
        {
            public GeometrySinkVtbl()
                : base(5)
            {
                AddMethod(new AddLineDelegate(AddLineImpl));
                AddMethod(new AddBezierDelegate(AddBezierImpl));
                AddMethod(new AddQuadraticBezierDelegate(AddQuadraticBezierImpl));
                AddMethod(new AddQuadraticBeziersDelegate(AddQuadraticBeziersImpl));
                AddMethod(new AddArcDelegate(AddArcImpl));
            }

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            private delegate void AddLineDelegate(IntPtr thisPtr, Vector2 point);

            private static void AddLineImpl(IntPtr thisPtr, Vector2 point)
            {
                var shadow = ToShadow<ID2D1GeometrySinkShadow>(thisPtr);
                var callback = (ID2D1GeometrySink)shadow.Callback;
                callback.AddLine(point);
            }

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            private delegate void AddBezierDelegate(IntPtr thisPtr, IntPtr bezier);

            private static void AddBezierImpl(IntPtr thisPtr, IntPtr bezier)
            {
                var shadow = ToShadow<ID2D1GeometrySinkShadow>(thisPtr);
                var callback = (ID2D1GeometrySink)shadow.Callback;
                callback.AddBezier(*((BezierSegment*)bezier));
            }

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            private delegate void AddQuadraticBezierDelegate(IntPtr thisPtr, IntPtr bezier);

            private static void AddQuadraticBezierImpl(IntPtr thisPtr, IntPtr bezier)
            {
                var shadow = ToShadow<ID2D1GeometrySinkShadow>(thisPtr);
                var callback = (ID2D1GeometrySink)shadow.Callback;
                callback.AddQuadraticBezier(*((QuadraticBezierSegment*)bezier));
            }

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            private delegate void AddQuadraticBeziersDelegate(IntPtr thisPtr, IntPtr beziers, int beziersCount);

            private static void AddQuadraticBeziersImpl(IntPtr thisPtr, IntPtr beziers, int beziersCount)
            {
                var shadow = ToShadow<ID2D1GeometrySinkShadow>(thisPtr);
                var callback = (ID2D1GeometrySink)shadow.Callback;
                var managedBeziers = new QuadraticBezierSegment[beziersCount];
                MemoryHelpers.Read(beziers, managedBeziers, 0, beziersCount);
                callback.AddQuadraticBeziers(managedBeziers);
            }

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            private delegate void AddArcDelegate(IntPtr thisPtr, IntPtr arc);

            private static void AddArcImpl(IntPtr thisPtr, IntPtr arc)
            {
                var shadow = ToShadow<ID2D1GeometrySinkShadow>(thisPtr);
                var callback = (ID2D1GeometrySink)shadow.Callback;
                callback.AddArc(*((ArcSegment*)arc));
            }
        }

        protected override CppObjectVtbl Vtbl => new GeometrySinkVtbl();
    }
}
