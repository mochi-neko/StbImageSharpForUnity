using System.Collections.Generic;
using StbSharp.StbImageSharp;

namespace Mochineko.StbImageSharpForUnity
{
    public static class AnimatedTextureConverter
    {
        /// <summary>
        /// Converts <see cref="AnimatedFrameResult"/> enumerable to <see cref="AnimatedTexture"/> enumerable.
        /// </summary>
        /// <param name="animatedFrameResult"></param>
        /// <param name="mipChain"></param>
        /// <param name="linear"></param>
        /// <returns></returns>
        /// <remarks>Not lazy.</remarks>
        public static IEnumerable<AnimatedTexture> ToAnimatedTextures(
            this IEnumerable<AnimatedFrameResult> animatedFrameResult,
            bool mipChain = false,
            bool linear = false)
        {
            // NOTE: Can load lazily to implement IEnumerable<T> directly. 
            var list = new List<AnimatedTexture>();
            foreach (var frame in animatedFrameResult)
            {
                list.Add(new AnimatedTexture(frame));
            }

            return list;
        }
    }
}