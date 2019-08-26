// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.
// Implementation based on https://github.com/tgjones/DotNetDxc

using System;

namespace Vortice.Dxc
{
    public readonly struct DxcShaderModel : IEquatable<DxcShaderModel>
    {
        public static readonly DxcShaderModel Model6_0 = new DxcShaderModel(6, 0);
        public static readonly DxcShaderModel Model6_1 = new DxcShaderModel(6, 1);
        public static readonly DxcShaderModel Model6_2 = new DxcShaderModel(6, 2);
        public static readonly DxcShaderModel Model6_3 = new DxcShaderModel(6, 3);
        public static readonly DxcShaderModel Model6_4 = new DxcShaderModel(6, 4);
        public static readonly DxcShaderModel Model6_5 = new DxcShaderModel(6, 5);

        public readonly int Major;
        public readonly int Minor;

        public DxcShaderModel(int major, int minor)
        {
            Major = major;
            Minor = minor;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is DxcShaderModel))
            {
                return false;
            }

            return Equals((DxcShaderModel)obj);
        }

        public bool Equals(DxcShaderModel other)
        {
            return Major == other.Major && Minor == other.Minor;
        }

        public static bool operator ==(DxcShaderModel left, DxcShaderModel right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(DxcShaderModel left, DxcShaderModel right)
        {
            return !(left == right);
        }
    }
}
