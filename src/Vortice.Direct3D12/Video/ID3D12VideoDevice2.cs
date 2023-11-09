// Copyright (c) Amer Koleci and contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.Direct3D12.Video;

/// <summary>
/// Provides video decoding and processing capabilities of a Microsoft Direct3D 12 device including the ability to query video capabilities and instantiating video decoders and processors.
/// This interface adds support for protected resources and video extension commands.
/// </summary>
public partial class ID3D12VideoDevice2
{
    #region CreateVideoDecoder
    public ID3D12VideoDecoder1 CreateVideoDecoder1(VideoDecoderDescription description, ID3D12ProtectedResourceSession protectedResourceSession)
    {
        CreateVideoDecoder1(ref description, protectedResourceSession, typeof(ID3D12VideoDecoder1).GUID, out IntPtr nativePtr).CheckError();
        return new ID3D12VideoDecoder1(nativePtr);
    }

    public T CreateVideoDecoder1<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] T>(VideoDecoderDescription description, ID3D12ProtectedResourceSession protectedResourceSession) where T : ID3D12VideoDecoder1
    {
        CreateVideoDecoder1(ref description, protectedResourceSession, typeof(T).GUID, out IntPtr nativePtr).CheckError();
        return MarshallingHelpers.FromPointer<T>(nativePtr)!;
    }

    public Result CreateVideoDecoder1<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] T>(VideoDecoderDescription description, ID3D12ProtectedResourceSession protectedResourceSession, out T? videoDecoder) where T : ID3D12VideoDecoder1
    {
        Result result = CreateVideoDecoder1(ref description, protectedResourceSession, typeof(T).GUID, out IntPtr nativePtr);
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
    public ID3D12VideoDecoderHeap1 CreateVideoDecoderHeap1(VideoDecoderHeapDescription description, ID3D12ProtectedResourceSession protectedResourceSession)
    {
        CreateVideoDecoderHeap1(ref description, protectedResourceSession, typeof(ID3D12VideoDecoderHeap1).GUID, out IntPtr nativePtr).CheckError();
        return new ID3D12VideoDecoderHeap1(nativePtr);
    }

    public T CreateVideoDecoderHeap1<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] T>(VideoDecoderHeapDescription description, ID3D12ProtectedResourceSession protectedResourceSession) where T : ID3D12VideoDecoderHeap1
    {
        CreateVideoDecoderHeap1(ref description, protectedResourceSession, typeof(T).GUID, out IntPtr nativePtr).CheckError();
        return MarshallingHelpers.FromPointer<T>(nativePtr)!;
    }

    public Result CreateVideoDecoderHeap1<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] T>(VideoDecoderHeapDescription description, ID3D12ProtectedResourceSession protectedResourceSession, out T? videoDecoder) where T : ID3D12VideoDecoderHeap1
    {
        Result result = CreateVideoDecoderHeap1(ref description, protectedResourceSession, typeof(T).GUID, out IntPtr nativePtr);
        if (result.Failure)
        {
            videoDecoder = default;
            return result;
        }

        videoDecoder = MarshallingHelpers.FromPointer<T>(nativePtr);
        return result;
    }
    #endregion

    #region CreateVideoExtensionCommand
    public ID3D12VideoExtensionCommand CreateVideoExtensionCommand(VideoExtensionCommandDescription description, IntPtr creationParameters, PointerSize creationParametersDataSizeInBytes, ID3D12ProtectedResourceSession protectedResourceSession)
    {
        CreateVideoExtensionCommand(ref description, creationParameters, creationParametersDataSizeInBytes, protectedResourceSession, typeof(ID3D12VideoExtensionCommand).GUID, out IntPtr nativePtr).CheckError();
        return new ID3D12VideoExtensionCommand(nativePtr);
    }

    public T CreateVideoExtensionCommand<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] T>(VideoExtensionCommandDescription description, IntPtr creationParameters, PointerSize creationParametersDataSizeInBytes, ID3D12ProtectedResourceSession protectedResourceSession) where T : ID3D12VideoExtensionCommand
    {
        CreateVideoExtensionCommand(ref description, creationParameters, creationParametersDataSizeInBytes, protectedResourceSession, typeof(T).GUID, out IntPtr nativePtr).CheckError();
        return MarshallingHelpers.FromPointer<T>(nativePtr)!;
    }

    public Result CreateVideoExtensionCommand<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] T>(
        VideoExtensionCommandDescription description,
        IntPtr creationParameters,
        PointerSize creationParametersDataSizeInBytes,
        ID3D12ProtectedResourceSession protectedResourceSession,
        out T? videoExtensionCommand) where T : ID3D12VideoExtensionCommand
    {
        Result result = CreateVideoExtensionCommand(ref description, creationParameters, creationParametersDataSizeInBytes, protectedResourceSession, typeof(T).GUID, out IntPtr nativePtr);
        if (result.Failure)
        {
            videoExtensionCommand = default;
            return result;
        }

        videoExtensionCommand = MarshallingHelpers.FromPointer<T>(nativePtr);
        return result;
    }
    #endregion

    #region CreateVideoProcessor1
    public ID3D12VideoProcessor CreateVideoProcessor1(
        int nodeMask,
        VideoProcessOutputStreamDescription outputStreamDescription,
        int inputStreamDescriptionsCount,
        VideoProcessInputStreamDescription[] inputStreamDescriptions,
        ID3D12ProtectedResourceSession protectedResourceSession)
    {
        CreateVideoProcessor1(
            nodeMask,
            ref outputStreamDescription,
            inputStreamDescriptionsCount,
            inputStreamDescriptions,
            protectedResourceSession,
            typeof(ID3D12VideoProcessor).GUID,
            out IntPtr nativePtr).CheckError();

        return new ID3D12VideoProcessor(nativePtr);
    }

    public ID3D12VideoProcessor CreateVideoProcessor1(
        int nodeMask,
        VideoProcessOutputStreamDescription outputStreamDescription,
        VideoProcessInputStreamDescription[] inputStreamDescriptions,
        ID3D12ProtectedResourceSession protectedResourceSession)
    {
        CreateVideoProcessor1(
            nodeMask,
            ref outputStreamDescription,
            inputStreamDescriptions.Length,
            inputStreamDescriptions,
            protectedResourceSession,
            typeof(ID3D12VideoProcessor).GUID,
            out IntPtr nativePtr).CheckError();
        return new ID3D12VideoProcessor(nativePtr);
    }

    public T CreateVideoProcessor1<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] T>(
        int nodeMask,
        VideoProcessOutputStreamDescription outputStreamDescription,
        VideoProcessInputStreamDescription[] inputStreamDescriptions,
        ID3D12ProtectedResourceSession protectedResourceSession) where T : ID3D12VideoProcessor
    {
        CreateVideoProcessor1(
            nodeMask,
            ref outputStreamDescription,
            inputStreamDescriptions.Length,
            inputStreamDescriptions,
            protectedResourceSession,
            typeof(T).GUID,
            out IntPtr nativePtr).CheckError();

        return MarshallingHelpers.FromPointer<T>(nativePtr)!;
    }

    public T CreateVideoProcessor1<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] T>(
        int nodeMask,
        VideoProcessOutputStreamDescription outputStreamDescription,
        int inputStreamDescriptionsCount,
        VideoProcessInputStreamDescription[] inputStreamDescriptions,
        ID3D12ProtectedResourceSession protectedResourceSession) where T : ID3D12VideoProcessor
    {
        CreateVideoProcessor1(
            nodeMask,
            ref outputStreamDescription,
            inputStreamDescriptionsCount,
            inputStreamDescriptions,
            protectedResourceSession,
            typeof(T).GUID,
            out IntPtr nativePtr).CheckError();
        return MarshallingHelpers.FromPointer<T>(nativePtr)!;
    }

    public Result CreateVideoProcessor1<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] T>(
        int nodeMask,
        VideoProcessOutputStreamDescription outputStreamDescription,
        VideoProcessInputStreamDescription[] inputStreamDescriptions,
        ID3D12ProtectedResourceSession protectedResourceSession,
        out T? videoDecoder) where T : ID3D12VideoProcessor
    {
        Result result = CreateVideoProcessor1(
            nodeMask,
            ref outputStreamDescription,
            inputStreamDescriptions.Length,
            inputStreamDescriptions,
            protectedResourceSession,
            typeof(T).GUID, out IntPtr nativePtr);
        if (result.Failure)
        {
            videoDecoder = default;
            return result;
        }

        videoDecoder = MarshallingHelpers.FromPointer<T>(nativePtr);
        return result;
    }

    public Result CreateVideoProcessor1<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] T>(
        int nodeMask,
        VideoProcessOutputStreamDescription outputStreamDescription,
        int inputStreamDescriptionsCount,
        VideoProcessInputStreamDescription[] inputStreamDescriptions,
        ID3D12ProtectedResourceSession protectedResourceSession,
        out T? videoDecoder) where T : ID3D12VideoProcessor1
    {
        Result result = CreateVideoProcessor1(
            nodeMask,
            ref outputStreamDescription,
            inputStreamDescriptionsCount,
            inputStreamDescriptions,
            protectedResourceSession,
            typeof(T).GUID, out IntPtr nativePtr);
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
