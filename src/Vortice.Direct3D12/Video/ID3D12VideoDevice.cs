// Copyright © Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.Direct3D12.Video;

/// <summary>
/// Provides video decoding and processing capabilities of a Microsoft Direct3D 12 device including the ability to query video capabilities and instantiating video decoders and processors.
/// </summary>
public partial class ID3D12VideoDevice
{
    public unsafe T CheckFeatureSupport<T>(FeatureVideo feature) where T : unmanaged
    {
        T featureSupport = default;
        CheckFeatureSupport(feature, &featureSupport, sizeof(T));
        return featureSupport;
    }

    public unsafe bool CheckFeatureSupport<T>(FeatureVideo feature, ref T featureSupport) where T : unmanaged
    {
        fixed (T* featureSupportPtr = &featureSupport)
        {
            return CheckFeatureSupport(feature, featureSupportPtr, sizeof(T)).Success;
        }
    }

    #region CreateVideoDecoder
    public ID3D12VideoDecoder CreateVideoDecoder(VideoDecoderDescription description)
    {
        CreateVideoDecoder(ref description, typeof(ID3D12VideoDecoder).GUID, out IntPtr nativePtr).CheckError();
        return new ID3D12VideoDecoder(nativePtr);
    }

    public Result CreateVideoDecoder(VideoDecoderDescription description, out ID3D12VideoDecoder? videoDecoder)
    {
        Result result = CreateVideoDecoder(ref description, typeof(ID3D12VideoDecoder).GUID, out IntPtr nativePtr);
        if (result.Failure)
        {
            videoDecoder = default;
            return result;
        }

        videoDecoder = new ID3D12VideoDecoder(nativePtr);
        return result;
    }

    public T CreateVideoDecoder<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] T>(VideoDecoderDescription description) where T : ID3D12VideoDecoder
    {
        CreateVideoDecoder(ref description, typeof(T).GUID, out IntPtr nativePtr).CheckError();
        return MarshallingHelpers.FromPointer<T>(nativePtr)!;
    }

    public Result CreateVideoDecoder<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] T>(VideoDecoderDescription description, out T? videoDecoder) where T : ID3D12VideoDecoder
    {
        Result result = CreateVideoDecoder(ref description, typeof(T).GUID, out IntPtr nativePtr);
        if (result.Failure)
        {
            videoDecoder = default;
            return result;
        }

        videoDecoder = MarshallingHelpers.FromPointer<T>(nativePtr);
        return result;
    }
    #endregion

    #region CreateVideoDecoderHeap
    public ID3D12VideoDecoderHeap CreateVideoDecoderHeap(VideoDecoderHeapDescription description)
    {
        CreateVideoDecoderHeap(ref description, typeof(ID3D12VideoDecoderHeap).GUID, out IntPtr nativePtr).CheckError();
        return new ID3D12VideoDecoderHeap(nativePtr);
    }

    public Result CreateVideoDecoderHeap(VideoDecoderHeapDescription description, out ID3D12VideoDecoderHeap? videoDecoder)
    {
        Result result = CreateVideoDecoderHeap(ref description, typeof(ID3D12VideoDecoderHeap).GUID, out IntPtr nativePtr);
        if (result.Failure)
        {
            videoDecoder = default;
            return result;
        }

        videoDecoder = new ID3D12VideoDecoderHeap(nativePtr);
        return result;
    }

    public T CreateVideoDecoderHeap<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] T>(VideoDecoderHeapDescription description) where T : ID3D12VideoDecoderHeap
    {
        CreateVideoDecoderHeap(ref description, typeof(T).GUID, out IntPtr nativePtr).CheckError();
        return MarshallingHelpers.FromPointer<T>(nativePtr)!;
    }

    public Result CreateVideoDecoderHeap<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] T>(VideoDecoderHeapDescription description, out T? videoDecoder) where T : ID3D12VideoDecoderHeap
    {
        Result result = CreateVideoDecoderHeap(ref description, typeof(T).GUID, out IntPtr nativePtr);
        if (result.Failure)
        {
            videoDecoder = default;
            return result;
        }

        videoDecoder = MarshallingHelpers.FromPointer<T>(nativePtr);
        return result;
    }
    #endregion

    #region CreateVideoProcessor
    public ID3D12VideoProcessor CreateVideoProcessor(int nodeMask, VideoProcessOutputStreamDescription outputStreamDescription, int inputStreamDescriptionsCount, VideoProcessInputStreamDescription[] inputStreamDescriptions)
    {
        CreateVideoProcessor(nodeMask, ref outputStreamDescription, inputStreamDescriptionsCount, inputStreamDescriptions, typeof(ID3D12VideoProcessor).GUID, out IntPtr nativePtr).CheckError();
        return new ID3D12VideoProcessor(nativePtr);
    }

    public ID3D12VideoProcessor CreateVideoProcessor(int nodeMask, VideoProcessOutputStreamDescription outputStreamDescription, VideoProcessInputStreamDescription[] inputStreamDescriptions)
    {
        CreateVideoProcessor(nodeMask, ref outputStreamDescription, inputStreamDescriptions.Length, inputStreamDescriptions, typeof(ID3D12VideoProcessor).GUID, out IntPtr nativePtr).CheckError();
        return new ID3D12VideoProcessor(nativePtr);
    }

    public T CreateVideoProcessor<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] T>(
        int nodeMask,
        VideoProcessOutputStreamDescription outputStreamDescription,
        VideoProcessInputStreamDescription[] inputStreamDescriptions) where T : ID3D12VideoProcessor
    {
        CreateVideoProcessor(nodeMask, ref outputStreamDescription, inputStreamDescriptions.Length, inputStreamDescriptions, typeof(T).GUID, out IntPtr nativePtr).CheckError();
        return MarshallingHelpers.FromPointer<T>(nativePtr)!;
    }

    public T CreateVideoProcessor<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] T>(
        int nodeMask,
        VideoProcessOutputStreamDescription outputStreamDescription,
        int inputStreamDescriptionsCount,
        VideoProcessInputStreamDescription[] inputStreamDescriptions) where T : ID3D12VideoProcessor
    {
        CreateVideoProcessor(nodeMask, ref outputStreamDescription, inputStreamDescriptionsCount, inputStreamDescriptions, typeof(T).GUID, out IntPtr nativePtr).CheckError();
        return MarshallingHelpers.FromPointer<T>(nativePtr)!;
    }

    public Result CreateVideoProcessor<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] T>(
        int nodeMask,
        VideoProcessOutputStreamDescription outputStreamDescription,
        VideoProcessInputStreamDescription[] inputStreamDescriptions,
        out T? videoDecoder) where T : ID3D12VideoProcessor
    {
        Result result = CreateVideoProcessor(nodeMask, ref outputStreamDescription, inputStreamDescriptions.Length, inputStreamDescriptions, typeof(T).GUID, out IntPtr nativePtr);
        if (result.Failure)
        {
            videoDecoder = default;
            return result;
        }

        videoDecoder = MarshallingHelpers.FromPointer<T>(nativePtr);
        return result;
    }

    public Result CreateVideoProcessor<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] T>(
        int nodeMask,
        VideoProcessOutputStreamDescription outputStreamDescription,
        int inputStreamDescriptionsCount,
        VideoProcessInputStreamDescription[] inputStreamDescriptions,
        out T? videoDecoder) where T : ID3D12VideoProcessor
    {
        Result result = CreateVideoProcessor(nodeMask, ref outputStreamDescription, inputStreamDescriptionsCount, inputStreamDescriptions, typeof(T).GUID, out IntPtr nativePtr);
        if (result.Failure)
        {
            videoDecoder = default;
            return result;
        }

        videoDecoder = MarshallingHelpers.FromPointer<T>(nativePtr);
        return result;
    }
    #endregion
}
