using System;
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
        /// <remarks>Run on main thread because it accesses Unity APIs.</remarks>
        public static Texture2D ToTexture2D(
            this ImageResult imageResult,
            bool mipChain = false,
            bool linear = false)
        {
            var texture = new Texture2D(
                width: imageResult.Width,
                height: imageResult.Height,
                textureFormat: imageResult.Comp.ToUnityTextureFormat(),
                mipChain: mipChain,
                linear: linear
            );

            texture.LoadRawTextureData(imageResult.Data);

            texture.Apply();

            return texture;
        }
        
        /// <summary>
        /// Set <see cref="ImageResult"/> into <see cref="Texture2D"/> that already been created.
        /// </summary>
        /// <param name="imageResult"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException">Throws when formats(width, height, texture format as Texture2D) have any mismatches between imageResult and target.</exception>
        /// <remarks>Run on main thread because it accesses Unity APIs.</remarks>
        public static void SetIntoTexture2D(
            this ImageResult imageResult,
            Texture2D target)
        {
            if (imageResult.Width != target.width)
            {
                throw new ArgumentException($"Widths are mismatch between image{imageResult.Width} and texture:{target.width}.");
            }
            if (imageResult.Height != target.height)
            {
                throw new ArgumentException($"Heights are mismatch between image{imageResult.Height} and texture:{target.height}.");
            }
            if (imageResult.Comp.ToUnityTextureFormat() != target.format)
            {
                throw new ArgumentException($"Texture formats are mismatch between image{imageResult.Comp.ToUnityTextureFormat()} and texture:{target.format}.");
            }
            
            target.LoadRawTextureData(imageResult.Data);

            target.Apply();
        }
    }
}