// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using Vortice.Mathematics;

namespace Vortice.Direct3D11
{
    /// <summary>
    /// Describes a query.
    /// </summary>
    public partial struct QueryDescription
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="QueryDescription"/> struct.
        /// </summary>
        /// <param name="queryType">Type of query (see <see cref="QueryType"/>).</param>
        /// <param name="miscFlags">Miscellaneous flags (see <see cref="QueryFlags"/>).</param>
        public QueryDescription(QueryType queryType, QueryFlags miscFlags = QueryFlags.None)
        {
            QueryType = queryType;
            MiscFlags = miscFlags;
        }
    }
}
