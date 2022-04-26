// Copyright © Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using System;
using System.Collections.Generic;
using System.Text;

namespace Vortice.DirectML;

public static class Dmlx
{
    public static long CalculateBufferTensorSize(
        TensorDataType dataType,
        int dimensionCount,
        int[] sizes,
        int[]? strides)
    {
        long elementSizeInBytes = 0;
        switch (dataType)
        {
            case TensorDataType.Float32:
            case TensorDataType.Uint32:
            case TensorDataType.Int32:
                elementSizeInBytes = 4;
                break;

            case TensorDataType.Float16:
            case TensorDataType.Uint16:
            case TensorDataType.Int16:
                elementSizeInBytes = 2;
                break;

            case TensorDataType.Uint8:
            case TensorDataType.Int8:
                elementSizeInBytes = 1;
                break;

            //case DML_TENSOR_DATA_TYPE_FLOAT64:
            //case DML_TENSOR_DATA_TYPE_UINT64:
            //case DML_TENSOR_DATA_TYPE_INT64:
            //    elementSizeInBytes = 8;
            //    break;

            default:
                return 0; // Invalid data type
        }

        long minimumImpliedSizeInBytes = 0;
        if (strides == null)
        {
            minimumImpliedSizeInBytes = 1;
            for (int i = 0; i < dimensionCount; i++)
            {
                minimumImpliedSizeInBytes *= sizes[i];
            }
            minimumImpliedSizeInBytes *= elementSizeInBytes;
        }
        else
        {
            int indexOfLastElement = 0;
            for (int i = 0; i < dimensionCount; i++)
            {
                indexOfLastElement += (sizes[i] - 1) * strides[i];
            }
            minimumImpliedSizeInBytes = (indexOfLastElement + 1) * elementSizeInBytes;
        }

        // Round up to the nearest 4 bytes.
        minimumImpliedSizeInBytes = (minimumImpliedSizeInBytes + 3) & ~3L;

        return minimumImpliedSizeInBytes;
    }
}
