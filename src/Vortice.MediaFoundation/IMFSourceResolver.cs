// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using SharpGen.Runtime;
using Vortice.Win32;

namespace Vortice.MediaFoundation
{
    public partial class IMFSourceResolver
    {
        public IUnknown CreateObjectFromURL(string url, SourceResolverFlags flags)
        {
            CreateObjectFromURL(url, (int)flags, null, out ObjectType objectType, out IUnknown @object).CheckError();
            return @object;
        }

        public IUnknown CreateObjectFromURL(string url, SourceResolverFlags flags, out ObjectType objectType)
        {
            CreateObjectFromURL(url, (int)flags, null, out objectType, out IUnknown @object).CheckError();
            return @object;
        }

        public IUnknown CreateObjectFromURL(string url, SourceResolverFlags flags, PropertyStore propertyStore, out ObjectType objectType)
        {
            CreateObjectFromURL(url, (int)flags, propertyStore, out objectType, out IUnknown @object).CheckError();
            return @object;
        }

        public Result CreateObjectFromURL(string url, SourceResolverFlags flags, PropertyStore propertyStore, out ObjectType objectType, out IUnknown @object)
        {
            return CreateObjectFromURL(url, (int)flags, propertyStore, out objectType, out @object);
        }

        public IUnknown CreateObjectFromByteStream(IByteStream stream, string url, SourceResolverFlags flags)
        {
            CreateObjectFromByteStream(stream, url, (int)flags, null, out ObjectType objectType, out IUnknown @object).CheckError();
            return @object;
        }

        public IUnknown CreateObjectFromByteStream(IByteStream stream, string url, SourceResolverFlags flags, out ObjectType objectType)
        {
            CreateObjectFromByteStream(stream, url, (int)flags, null, out objectType, out IUnknown @object).CheckError();
            return @object;
        }

        public IUnknown CreateObjectFromByteStream(IByteStream stream, string url, SourceResolverFlags flags, PropertyStore propertyStore, out ObjectType objectType)
        {
            CreateObjectFromByteStream(stream, url, (int)flags, propertyStore, out objectType, out IUnknown @object).CheckError();
            return @object;
        }

        public Result CreateObjectFromByteStream(IByteStream stream, string url, SourceResolverFlags flags, PropertyStore propertyStore, out ObjectType objectType, out IUnknown @object)
        {
            return CreateObjectFromByteStream(stream, url, (int)flags, propertyStore, out objectType, out @object);
        }
    }
}
