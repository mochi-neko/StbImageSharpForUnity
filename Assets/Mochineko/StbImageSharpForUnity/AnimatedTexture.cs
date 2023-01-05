using System;
using StbImageSharp;
using UnityEngine;

namespace Mochineko.StbImageSharpForUnity
{
    /// <summary>
    /// An animated <see cref="Texture2D"/> with frame delay.
    /// </summary>
    public sealed class AnimatedTexture : IDisposable
    {
        public Texture2D Texture { get; private set; }
        public int DelayInMs { get; }

        public AnimatedTexture(AnimatedFrameResult animatedFrameResult, bool mipChain, bool linear)
        {
            Texture = animatedFrameResult.ToTexture2D(mipChain, linear);
            DelayInMs = animatedFrameResult.DelayInMs;
        }

        public void Dispose()
        {
            UnityEngine.Object.Destroy(Texture);
            Texture = null;
        }
    }
}