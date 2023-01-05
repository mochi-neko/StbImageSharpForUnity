using System.Collections.Generic;
using StbImageSharp;

namespace Mochineko.StbImageSharpForUnity
{
    public static class AnimatedTextureConverter
    {
        /// <summary>
        /// Converts <see cref="AnimatedFrameResult"/> enumerable to <see cref="AnimatedTexture"/> enumerable.
        /// If you would decode large file, I recommend to use <see cref="ToTextureSharedAnimatedTextures"/> instead.
        /// </summary>
        /// <param name="animatedFrameResults"></param>
        /// <param name="mipChain"></param>
        /// <param name="linear"></param>
        /// <returns></returns>
        /// <remarks>Are not lazily evaluated.</remarks>
        public static IEnumerable<AnimatedTexture> ExpandAllToAnimatedTextures(
            this IEnumerable<AnimatedFrameResult> animatedFrameResults,
            bool mipChain = false,
            bool linear = false)
        {
            // NOTICE: Does not evaluate lazily. So if a large gif file is loaded, you spends large memory.
            var list = new List<AnimatedTexture>();
            foreach (var frame in animatedFrameResults)
            {
                list.Add(new AnimatedTexture(frame, mipChain, linear));
            }

            return list;
        }
        
       /// <summary>
       /// Converts <see cref="AnimatedFrameResult"/> enumerable to <see cref="TextureSharedAnimatedTexture"/> enumerable.
       /// </summary>
       /// <param name="enumerable"></param>
       /// <param name="mipChain"></param>
       /// <param name="linear"></param>
       /// <returns></returns>
        public static IEnumerable<TextureSharedAnimatedTexture> ToTextureSharedAnimatedTextures(
            this IEnumerable<AnimatedFrameResult> enumerable,
            bool mipChain = false,
            bool linear = false)
            => new TextureSharedAnimatedTextureEnumerable(enumerable, mipChain, linear);
    }
}