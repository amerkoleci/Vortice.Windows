// Copyright (c) Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.DirectStorage;

public partial struct Request
{
    [StructLayout(LayoutKind.Sequential, Pack = 0, CharSet = CharSet.Unicode)]
    internal partial struct __Native
    {
        public RequestOptions Options;
        public Source.__Native Source;
        public Destination.__Native Destination;
        public uint UncompressedSize;
        public ulong CancellationTag;
        public IntPtr Name;
    }

    internal unsafe void __MarshalFree(ref __Native @ref)
    {
        switch (Options.SourceType)
        {
            case RequestSourceType.File:
                Source.File.__MarshalFree(ref @ref.Source.File);
                break;
        }
        switch (Options.DestinationType)
        {
            case RequestDestinationType.Buffer:
                Destination.Buffer.__MarshalFree(ref @ref.Destination.Buffer);
                break;
            case RequestDestinationType.TextureRegion:
                Destination.Texture.__MarshalFree(ref @ref.Destination.Texture);
                break;
            case RequestDestinationType.MultipleSubresources:
                Destination.MultipleSubresources.__MarshalFree(ref @ref.Destination.MultipleSubresources);
                break;
            case RequestDestinationType.Tiles:
                Destination.Tiles.__MarshalFree(ref @ref.Destination.Tiles);
                break;
        }
        Marshal.FreeHGlobal(@ref.Name);
    }

    internal unsafe void __MarshalFrom(ref __Native @ref)
    {
        Options = @ref.Options;
        switch (Options.SourceType)
        {
            case RequestSourceType.File:
                Source.File.__MarshalFrom(ref @ref.Source.File);
                break;
            case RequestSourceType.Memory:
                Source.Memory = @ref.Source.Memory;
                break;
        }
        switch (Options.DestinationType)
        {
            case RequestDestinationType.Memory:
                Destination.Memory = @ref.Destination.Memory;
                break;
            case RequestDestinationType.Buffer:
                Destination.Buffer.__MarshalFrom(ref @ref.Destination.Buffer);
                break;
            case RequestDestinationType.TextureRegion:
                Destination.Texture.__MarshalFrom(ref @ref.Destination.Texture);
                break;
            case RequestDestinationType.MultipleSubresources:
                Destination.MultipleSubresources.__MarshalFrom(ref @ref.Destination.MultipleSubresources);
                break;
            case RequestDestinationType.Tiles:
                Destination.Tiles.__MarshalFrom(ref @ref.Destination.Tiles);
                break;
        }
        UncompressedSize = @ref.UncompressedSize;
        CancellationTag = @ref.CancellationTag;
        Name = Marshal.PtrToStringAnsi(@ref.Name);
    }

    internal unsafe void __MarshalTo(ref __Native @ref)
    {
        @ref.Options = Options;
        switch (Options.SourceType)
        {
            case RequestSourceType.File:
                Source.File.__MarshalTo(ref @ref.Source.File);
                break;
            case RequestSourceType.Memory:
                @ref.Source.Memory = Source.Memory;
                break;
        }
        switch (Options.DestinationType)
        {
            case RequestDestinationType.Memory:
                @ref.Destination.Memory = Destination.Memory;
                break;
            case RequestDestinationType.Buffer:
                Destination.Buffer.__MarshalTo(ref @ref.Destination.Buffer);
                break;
            case RequestDestinationType.TextureRegion:
                Destination.Texture.__MarshalTo(ref @ref.Destination.Texture);
                break;
            case RequestDestinationType.MultipleSubresources:
                Destination.MultipleSubresources.__MarshalTo(ref @ref.Destination.MultipleSubresources);
                break;
            case RequestDestinationType.Tiles:
                Destination.Tiles.__MarshalTo(ref @ref.Destination.Tiles);
                break;
        }
        @ref.UncompressedSize = UncompressedSize;
        @ref.CancellationTag = CancellationTag;
        @ref.Name = Marshal.StringToHGlobalAnsi(Name);
    }
}
