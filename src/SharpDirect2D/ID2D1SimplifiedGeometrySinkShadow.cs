// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using System.Runtime.InteropServices;
using SharpGen.Runtime;
using System.Numerics;

namespace SharpDirect2D
{
    internal class ID2D1SimplifiedGeometrySinkShadow : ComObjectShadow
    {
        /// <summary>
        /// Return a pointer to the unmanaged version of this callback.
        /// </summary>
        /// <param name="callback">The callback.</param>
        /// <returns>A pointer to a shadow c++ callback</returns>
        public static IntPtr ToIntPtr(ID2D1SimplifiedGeometrySink callback)
        {
            return ToCallbackPtr<ID2D1SimplifiedGeometrySink>(callback);
        }

        protected unsafe class SimplifiedGeometrySinkVtbl : ComObjectVtbl
        {
            public SimplifiedGeometrySinkVtbl(int nbMethods)
                : base(nbMethods + 7)
            {
                AddMethod(new SetFillModeDelegate(SetFillModeImpl));
                AddMethod(new SetSegmentFlagsDelegate(SetSegmentFlagsImpl));
                AddMethod(new BeginFigureDelegate(BeginFigureImpl));
                AddMethod(new AddLinesDelegate(AddLinesImpl));
                AddMethod(new AddBeziersDelegate(AddBeziersImpl));
                AddMethod(new EndFigureDelegate(EndFigureImpl));
                AddMethod(new CloseDelegate(CloseImpl));
            }

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            private delegate void SetFillModeDelegate(IntPtr thisPtr, FillMode fillMode);

            private static void SetFillModeImpl(IntPtr thisPtr, FillMode fillMode)
            {
                var shadow = ToShadow<ID2D1SimplifiedGeometrySinkShadow>(thisPtr);
                var callback = (ID2D1SimplifiedGeometrySink)shadow.Callback;
                callback.SetFillMode(fillMode);
            }

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            private delegate void SetSegmentFlagsDelegate(IntPtr thisPtr, PathSegment vertexFlags);

            private static void SetSegmentFlagsImpl(IntPtr thisPtr, PathSegment vertexFlags)
            {
                var shadow = ToShadow<ID2D1SimplifiedGeometrySinkShadow>(thisPtr);
                var callback = (ID2D1SimplifiedGeometrySink)shadow.Callback;
                callback.SetSegmentFlags(vertexFlags);
            }

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            private delegate void BeginFigureDelegate(IntPtr thisPtr, Vector2 startPoint, FigureBegin figureBegin);

            private static void BeginFigureImpl(IntPtr thisPtr, Vector2 startPoint, FigureBegin figureBegin)
            {
                var shadow = ToShadow<ID2D1SimplifiedGeometrySinkShadow>(thisPtr);
                var callback = (ID2D1SimplifiedGeometrySink)shadow.Callback;
                callback.BeginFigure(startPoint, figureBegin);
            }

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            private delegate void AddLinesDelegate(IntPtr thisPtr, IntPtr points, int pointsCount);
            private static void AddLinesImpl(IntPtr thisPtr, IntPtr points, int pointsCount)
            {
                var shadow = ToShadow<ID2D1SimplifiedGeometrySinkShadow>(thisPtr);
                var callback = (ID2D1SimplifiedGeometrySink)shadow.Callback;
                var managedPoints = new Vector2[pointsCount];
                MemoryHelpers.Read(points, managedPoints, 0, pointsCount);
                callback.AddLines(managedPoints);
            }

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            private delegate void AddBeziersDelegate(IntPtr thisPtr, IntPtr beziers, int beziersCount);
            private static void AddBeziersImpl(IntPtr thisPtr, IntPtr beziers, int beziersCount)
            {
                var shadow = ToShadow<ID2D1SimplifiedGeometrySinkShadow>(thisPtr);
                var callback = (ID2D1SimplifiedGeometrySink)shadow.Callback;
                var managedBeziers = new BezierSegment[beziersCount];
                MemoryHelpers.Read(beziers, managedBeziers, 0, beziersCount);
                callback.AddBeziers(managedBeziers);
            }

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            private delegate void EndFigureDelegate(IntPtr thisPtr, FigureEnd figureEnd);

            private static void EndFigureImpl(IntPtr thisPtr, FigureEnd figureEnd)
            {
                var shadow = ToShadow<ID2D1SimplifiedGeometrySinkShadow>(thisPtr);
                var callback = (ID2D1SimplifiedGeometrySink)shadow.Callback;
                callback.EndFigure(figureEnd);
            }

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            private delegate int CloseDelegate(IntPtr thisPtr);
            private static int CloseImpl(IntPtr thisPtr)
            {
                try
                {
                    var shadow = ToShadow<ID2D1SimplifiedGeometrySinkShadow>(thisPtr);
                    var callback = (ID2D1SimplifiedGeometrySink)shadow.Callback;
                    callback.Close();
                }
                catch (Exception exception)
                {
                    return (int)Result.GetResultFromException(exception);
                }
                return Result.Ok.Code;
            }
        }

        protected override CppObjectVtbl Vtbl => new SimplifiedGeometrySinkVtbl(0);
    }
}
