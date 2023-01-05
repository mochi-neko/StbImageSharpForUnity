using System;
using System.Collections;
using System.Collections.Generic;
using StbImageSharp;
using UnityEngine;

namespace Mochineko.StbImageSharpForUnity
{
    /// <summary>
    /// An implementation of <see cref="IEnumerator{T}"/> for <see cref="TextureSharedAnimatedTexture"/>.
    /// </summary>
    internal sealed class TextureSharedAnimatedTextureEnumerator : IEnumerator<TextureSharedAnimatedTexture>
    {
        private readonly IEnumerator<AnimatedFrameResult> enumerator;
        private readonly bool mipChain;
        private readonly bool linear;

        private TextureSharedAnimatedTexture current;
        TextureSharedAnimatedTexture IEnumerator<TextureSharedAnimatedTexture>.Current => current;
        object IEnumerator.Current => current;

        private Texture2D sharedTexture = null;

        public TextureSharedAnimatedTextureEnumerator(
            IEnumerator<AnimatedFrameResult> enumerator,
            bool mipChain = false,
            bool linear = false)
        {
            this.enumerator = enumerator ?? throw new ArgumentNullException(nameof(enumerator));
            this.mipChain = mipChain;
            this.linear = linear;
        }

        // NOTICE: This has to be called on the main thread of Unity because it accesses Unity APIs for Texture2D.
        bool IEnumerator.MoveNext()
        {
            if (enumerator.MoveNext())
            {
                var currentFrame = enumerator.Current;

                if (sharedTexture == null)
                {
                    sharedTexture = currentFrame.ToTexture2D(mipChain, linear);
                }
                
                current = new TextureSharedAnimatedTexture(enumerator.Current, sharedTexture);

                return true;
            }
            else
            {
                return false;
            }
        }

        void IEnumerator.Reset()
        {
            // NOTE: Not implemented Reset() at AnimatedGifEnumerator.
            throw new NotImplementedException();
        }

        void IDisposable.Dispose()
        {
            enumerator.Dispose();

            if (sharedTexture != null)
            {
                UnityEngine.Object.Destroy(sharedTexture);
                sharedTexture = null;
            }
        }
    }
}