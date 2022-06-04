using System.IO;
using StbImageSharp;

namespace Mochineko.StbImageSharpForUnity
{
    public static class ImageDecoder
    {
        /// <summary>
        /// Decodes an image from <see cref="Stream"/> to <see cref="ImageResult"/>.
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="requiredColorComponents"></param>
        /// <returns></returns>
        public static ImageResult DecodeImage(
            Stream stream,
            ColorComponents requiredColorComponents = ColorComponents.Default)
        {
            // Flips vertical direction of image along with Unity coordinates.
            StbImage.stbi_set_flip_vertically_on_load(1);
            
            return ImageResult.FromStream(stream, requiredColorComponents);
        }

        /// <summary>
        /// Decodes an image from <see cref="byte"/> array to <see cref="ImageResult"/>.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="requiredColorComponents"></param>
        /// <returns></returns>
        public static ImageResult DecodeImage(
            byte[] data,
            ColorComponents requiredColorComponents = ColorComponents.Default)
        {
            using var stream = new MemoryStream(data);
            return DecodeImage(stream, requiredColorComponents);
        }
        
        
    }
}