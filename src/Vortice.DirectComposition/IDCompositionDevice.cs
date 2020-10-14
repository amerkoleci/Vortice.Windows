// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using SharpGen.Runtime;
using SharpGen.Runtime.Win32;

namespace Vortice.DirectComposition
{
    public partial class IDCompositionDevice
    {
        public bool CheckDeviceState()
        {
            CheckDeviceState(out RawBool result).CheckError();
            return result;
        }

        public IDCompositionAnimation CreateAnimation()
        {
            CreateAnimation(out IDCompositionAnimation animation).CheckError();
            return animation;
        }

        public IDCompositionEffectGroup CreateEffectGroup()
        {
            CreateEffectGroup(out IDCompositionEffectGroup effectGroup).CheckError();
            return effectGroup;
        }

        public IDCompositionMatrixTransform CreateMatrixTransform()
        {
            CreateMatrixTransform(out IDCompositionMatrixTransform matrixTransform).CheckError();
            return matrixTransform;
        }

        public IDCompositionMatrixTransform3D CreateMatrixTransform3D()
        {
            CreateMatrixTransform3D(out IDCompositionMatrixTransform3D matrixTransform).CheckError();
            return matrixTransform;
        }

        public IDCompositionRectangleClip CreateRectangleClip()
        {
            CreateRectangleClip(out IDCompositionRectangleClip clip).CheckError();
            return clip;
        }

        public IDCompositionRotateTransform CreateRotateTransform()
        {
            CreateRotateTransform(out IDCompositionRotateTransform rotateTransform).CheckError();
            return rotateTransform;
        }

        public IDCompositionRotateTransform3D CreateRotateTransform3D()
        {
            CreateRotateTransform3D(out IDCompositionRotateTransform3D rotateTransform).CheckError();
            return rotateTransform;
        }

        public IDCompositionScaleTransform CreateScaleTransform()
        {
            CreateScaleTransform(out IDCompositionScaleTransform scaleTransform).CheckError();
            return scaleTransform;
        }

        public IDCompositionScaleTransform3D CreateScaleTransform3D()
        {
            CreateScaleTransform3D(out IDCompositionScaleTransform3D scaleTransform).CheckError();
            return scaleTransform;
        }

        public IDCompositionSkewTransform CreateSkewTransform()
        {
            CreateSkewTransform(out IDCompositionSkewTransform skewTransform).CheckError();
            return skewTransform;
        }
    }
}
