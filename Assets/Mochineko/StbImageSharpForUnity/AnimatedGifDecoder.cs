using System;
using System.Collections.Generic;
using System.IO;
using StbImageSharp;

namespace Mochineko.StbImageSharpForUnity
{
    public static class AnimatedGifDecoder
    {
        /// <summary>
        /// Decodes an GIF image from <see cref="Stream"/> to <see cref="AnimatedFrameResult"/> enumerable.
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="requiredColorComponents"></param>
        /// <returns></returns>
        public static (IEnumerable<AnimatedFrameResult> enumerable, IDisposable disposable) 
            DecodeGifImage(
                Stream stream,
                ColorComponents requiredColorComponents = ColorComponents.Default)
        {
            // FIXME: It does not work for GIF.
            //StbImage.stbi_set_flip_vertically_on_load(1);
            
            return (ImageResult.AnimatedGifFramesFromStream(stream, requiredColorComponents), stream);
        }
        
        /// <summary>
        /// Decodes an GIF image from <see cref="byte"/> array to <see cref="AnimatedFrameResult"/> enumerable.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="requiredColorComponents"></param>
        /// <returns></returns>
        public static (IEnumerable<AnimatedFrameResult> enumerable, IDisposable disposable)
            DecodeGifImage(
                byte[] data,
                ColorComponents requiredColorComponents = ColorComponents.Default)
        {
            var stream = new MemoryStream(data);
            return DecodeGifImage(stream, requiredColorComponents);
        }
    }
}