// Copyright (c) Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.Direct3D9;

public partial class IDirect3DVertexDeclaration9
{
    public int ElementsCount
    {
        get
        {
            int count = 0;
            GetDeclaration(null, ref count);
            return count;
        }
    }

    /// <summary>
    /// Gets the <see cref="VertexElement"/> array.
    /// </summary>
    public VertexElement[]? Elements
    {
        get
        {
            int count = 0;
            GetDeclaration(null, ref count);
            if (count == 0)
            {
                return null;
            }

            var buffer = new VertexElement[count];
            GetDeclaration(buffer, ref count);

            return buffer;
        }
    }

    public int GetElements(VertexElement[] elements)
    {
        int count = elements.Length;
        GetDeclaration(elements, ref count);
        return count;
    }
}
