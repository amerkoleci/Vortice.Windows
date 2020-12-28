// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using SharpGen.Runtime;

namespace Vortice.Direct3D12.Video
{
    /// <summary>
    /// Provides video decoding and processing capabilities of a Microsoft Direct3D 12 device including the ability to query video capabilities and instantiating video decoders and processors.
    /// This interface adds support for motion estimation.
    /// </summary>
    public partial class ID3D12VideoDevice1
    {
        public ID3D12VideoMotionEstimator CreateVideoMotionEstimator(VideoMotionEstimatorDescription description, ID3D12ProtectedResourceSession protectedResourceSession)
        {
            Result result = CreateVideoMotionEstimator(ref description, protectedResourceSession, typeof(ID3D12VideoMotionEstimator).GUID, out IntPtr nativePtr);
            if (result.Failure)
                return default;

            return new ID3D12VideoMotionEstimator(nativePtr);
        }

        public T CreateVideoMotionEstimator<T>(VideoMotionEstimatorDescription description, ID3D12ProtectedResourceSession protectedResourceSession) where T : ID3D12VideoMotionEstimator
        {
            Result result = CreateVideoMotionEstimator(ref description, protectedResourceSession, typeof(T).GUID, out IntPtr nativePtr);
            if (result.Failure)
                return default;

            return FromPointer<T>(nativePtr);
        }

        public Result CreateVideoMotionEstimator<T>(VideoMotionEstimatorDescription description, ID3D12ProtectedResourceSession protectedResourceSession, out T videoMotionEstimator) where T : ID3D12VideoMotionEstimator
        {
            Result result = CreateVideoMotionEstimator(ref description, protectedResourceSession, typeof(T).GUID, out IntPtr nativePtr);
            if (result.Failure)
            {
                videoMotionEstimator = default;
                return result;
            }

            videoMotionEstimator = FromPointer<T>(nativePtr);
            return result;
        }

        public ID3D12VideoMotionVectorHeap CreateVideoMotionVectorHeap(VideoMotionVectorHeapDescription description, ID3D12ProtectedResourceSession protectedResourceSession)
        {
            Result result = CreateVideoMotionVectorHeap(ref description, protectedResourceSession, typeof(ID3D12VideoMotionVectorHeap).GUID, out IntPtr nativePtr);
            if (result.Failure)
                return default;

            return new ID3D12VideoMotionVectorHeap(nativePtr);
        }

        public T CreateVideoMotionVectorHeap<T>(VideoMotionVectorHeapDescription description, ID3D12ProtectedResourceSession protectedResourceSession) where T : ID3D12VideoMotionVectorHeap
        {
            Result result = CreateVideoMotionVectorHeap(ref description, protectedResourceSession, typeof(T).GUID, out IntPtr nativePtr);
            if (result.Failure)
                return default;

            return FromPointer<T>(nativePtr);
        }

        public Result CreateVideoMotionVectorHeap<T>(VideoMotionVectorHeapDescription description, ID3D12ProtectedResourceSession protectedResourceSession, out T videoMotionEstimator) where T : ID3D12VideoMotionVectorHeap
        {
            Result result = CreateVideoMotionVectorHeap(ref description, protectedResourceSession, typeof(T).GUID, out IntPtr nativePtr);
            if (result.Failure)
            {
                videoMotionEstimator = default;
                return result;
            }

            videoMotionEstimator = FromPointer<T>(nativePtr);
            return result;
        }
    }
}
