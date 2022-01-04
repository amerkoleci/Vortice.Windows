// Copyright © Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.Direct3D11;

/// <summary>
/// Describes a query.
/// </summary>
public partial struct QueryDescription1
{
    /// <summary>
    /// Initializes a new instance of the <see cref="QueryDescription"/> struct.
    /// </summary>
    /// <param name="queryType">Type of query (see <see cref="QueryType"/>).</param>
    /// <param name="miscFlags">Miscellaneous flags (see <see cref="QueryFlags"/>).</param>
    /// <param name="contextType"></param>
    public QueryDescription1(QueryType queryType, QueryFlags miscFlags = QueryFlags.None, ContextType contextType = ContextType.All)
    {
        QueryType = queryType;
        MiscFlags = miscFlags;
        ContextType = contextType;
    }
}
