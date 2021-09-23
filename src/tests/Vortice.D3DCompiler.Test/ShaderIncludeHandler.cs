﻿// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System.Collections.Generic;
using System.IO;
using SharpGen.Runtime;
using Vortice.Direct3D;

namespace Vortice.D3DCompiler.Test
{
    public class ShaderIncludeHandler : CallbackBase, Include
    {
        private readonly List<string> _includeDirectories;

        public ShaderIncludeHandler(params string[] includeDirectories)
        {
            _includeDirectories = new(includeDirectories);
        }

        public Stream Open(IncludeType type, string fileName, Stream parentStream)
        {
            var includeFile = GetFilePath(fileName);

            if (!File.Exists(includeFile))
                throw new FileNotFoundException($"Include file '{fileName}' not found.");

            var includeStream = new FileStream(includeFile, FileMode.Open, FileAccess.Read);

            return includeStream;
        }

        private string? GetFilePath(string fileName)
        {
            if (File.Exists(fileName))
                return fileName;

            for (int i = 0; i < _includeDirectories.Count; i++)
            {
                var filePath = Path.GetFullPath(Path.Combine(_includeDirectories[i], fileName));

                if (File.Exists(filePath))
                {
                    var fileDirectory = Path.GetDirectoryName(filePath);

                    if (!_includeDirectories.Contains(fileDirectory))
                        _includeDirectories.Add(fileDirectory);

                    return filePath;
                }
            }

            return null;
        }

        public void Close(Stream stream)
        {
            stream.Dispose();
        }
    }
}
