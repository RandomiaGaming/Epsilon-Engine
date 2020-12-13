﻿using System.IO;
namespace EpsilonEngine.Modules.PNGCodec
{
    public sealed class AudioAsset : AssetBase
    {
        public readonly AudioClip data = null;

        public AudioAsset(Stream stream, string name, AudioClip data) : base(stream, name)
        {
            this.data = data;
        }
    }
}