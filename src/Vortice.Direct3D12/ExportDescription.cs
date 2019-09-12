// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

namespace Vortice.Direct3D12
{
    /// <summary>
    /// Describes an export from a state subobject such as a DXIL library or a collection state object.
    /// </summary>
    public partial struct ExportDescription
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ExportDescription"/> struct.
        /// </summary>
        /// <param name="name">
        /// The name to be exported. If the name refers to a function that is overloaded, a modified version of the name (e.g. encoding function parameter information in name string) can be provided to disambiguate which overload to use. The modified name for a function can be retrieved using HLSL compiler reflection.
        /// If the ExportToRename field is non-null, Name refers to the new name to use for it when exported. In this case Name must be the unmodified name, whereas ExportToRename can be either a modified or unmodified name. A given internal name may be exported multiple times with different renames (and/or not renamed).
        /// </param>
        /// <param name="exportToRename">If non-null, this is the name of an export to use but then rename when exported.</param>
        /// <param name="flags">The flags to apply to the export.</param>
        public ExportDescription(string name, string exportToRename, ExportFlags flags = ExportFlags.None)
        {
            Name = name;
            ExportToRename = exportToRename;
            Flags = flags;
        }
    }
}
