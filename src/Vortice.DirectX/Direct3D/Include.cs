// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System.IO;
using SharpGen.Runtime;

namespace Vortice.Direct3D
{
    public partial interface Include
    {
        /// <summary>
        /// A user-implemented method for opening and reading the contents of a shader #include file.
        /// </summary>
        /// <param name="type">A <see cref="IncludeType"/>-typed value that indicates the location of the #include file.</param>
        /// <param name="fileName">Name of the #include file.</param>
        /// <param name="parentStream">Pointer to the container that includes the #include file.</param>
        /// <returns>Stream that is associated with fileName to be read. This reference remains valid until <see cref="Include.Close"/> is called.</returns>
        /// <unmanaged>HRESULT Open([None] D3D_INCLUDE_TYPE IncludeType,[None] const char* pFileName,[None] LPCVOID pParentData,[None] LPCVOID* ppData,[None] UINT* pBytes)</unmanaged>
        Stream Open(IncludeType type, string fileName, Stream? parentStream);

        /// <summary>
        /// A user-implemented method for closing a shader #include file.
        /// </summary>
        /// <remarks>
        /// If <see cref="Include.Open"/> was successful, Close is guaranteed to be called before the API using the <see cref="Include"/> interface returns.
        /// </remarks>
        /// <param name="stream">This is a reference that was returned by the corresponding <see cref="Include.Open"/> call.</param>
        /// <unmanaged>HRESULT Close([None] LPCVOID pData)</unmanaged>
        void Close(Stream stream);
    }
}
