using System;
using System.Collections;
using System.Collections.Generic;
using StbImageSharp;

namespace Mochineko.StbImageSharpForUnity
{
    /// <summary>
    /// An implementation of <see cref="IEnumerable{T}"/> for <see cref="TextureSharedAnimatedTexture"/>.
    /// </summary>
    /// <seealso cref="TextureSharedAnimatedTextureEnumerator"/>
    internal sealed class TextureSharedAnimatedTextureEnumerable : IEnumerable<TextureSharedAnimatedTexture>
    {
        private readonly IEnumerable<AnimatedFrameResult> enumerable;
        private readonly bool mipChain;
        private readonly bool linear;
        
        public TextureSharedAnimatedTextureEnumerable(
            IEnumerable<AnimatedFrameResult> enumerable,
            bool mipChain = false,
            bool linear = false)
        {
            this.enumerable = enumerable ?? throw new ArgumentNullException(nameof(enumerable));
            this.mipChain = mipChain;
            this.linear = linear;
        }

        IEnumerator<TextureSharedAnimatedTexture> IEnumerable<TextureSharedAnimatedTexture>.GetEnumerator()
            => new TextureSharedAnimatedTextureEnumerator(enumerable.GetEnumerator(), mipChain, linear);

        IEnumerator IEnumerable.GetEnumerator()
            => (this as IEnumerable<TextureSharedAnimatedTexture>).GetEnumerator();
    }
}