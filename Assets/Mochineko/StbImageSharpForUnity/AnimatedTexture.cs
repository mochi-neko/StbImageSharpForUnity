using System;
using StbSharp.StbImageSharp;
using UnityEngine;

namespace Mochineko.StbImageSharpForUnity
{
    /// <summary>
    /// An animated <see cref="Texture2D"/> with frame delay.
    /// </summary>
    public sealed class AnimatedTexture : IDisposable
    {
        public Texture2D Texture { get; }
        public int DelayInMs { get; }

        public AnimatedTexture(AnimatedFrameResult animatedFrameResult)
        {
            Texture = animatedFrameResult.ToTexture2D();
            DelayInMs = animatedFrameResult.DelayInMs;
        }

        public void Dispose()
        {
            UnityEngine.Object.Destroy(Texture);
        }
    }
}