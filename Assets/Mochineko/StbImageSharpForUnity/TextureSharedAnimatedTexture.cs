using StbImageSharp;
using UnityEngine;

namespace Mochineko.StbImageSharpForUnity
{
    /// <summary>
    /// An animated shared <see cref="Texture2D"/> with frame delay.
    /// </summary>
    public sealed class TextureSharedAnimatedTexture
    {
        public Texture2D Texture { get; }
        public int DelayInMs { get; }

        public TextureSharedAnimatedTexture(AnimatedFrameResult animatedFrameResult, Texture2D sharedTexture)
        {
            animatedFrameResult.SetIntoTexture2D(sharedTexture);
            Texture = sharedTexture;
            DelayInMs = animatedFrameResult.DelayInMs;
        }
    }
}