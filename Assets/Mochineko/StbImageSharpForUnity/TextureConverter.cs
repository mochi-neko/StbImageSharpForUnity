using StbImageSharp;
using UnityEngine;

namespace Mochineko.StbImageSharpForUnity
{
    public static class TextureConverter
    {
        /// <summary>
        /// Converts <see cref="ImageResult"/> to <see cref="Texture2D"/>.
        /// </summary>
        /// <param name="imageResult"></param>
        /// <param name="mipChain"></param>
        /// <param name="linear"></param>
        /// <returns></returns>
        /// <remarks>Run on main thread because it accesses Unity API.</remarks>
        public static Texture2D ToTexture2D(
            this ImageResult imageResult,
            bool mipChain = false,
            bool linear = false)
        {
            var texture = new Texture2D(
                width:imageResult.Width,
                height:imageResult.Height,
                textureFormat:imageResult.Comp.ToUnityTextureFormat(),
                mipChain:mipChain,
                linear:linear
            );
            
            texture.LoadRawTextureData(imageResult.Data);
            
            texture.Apply();

            return texture;
        }
    }
}