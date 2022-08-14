using System;
using StbSharp.StbImageSharp;
using UnityEngine;

namespace Mochineko.StbImageSharpForUnity
{
    public static class FormatConverter
    {
        /// <summary>
        /// Converts <see cref="ColorComponents"/> to <see cref="TextureFormat"/>.
        /// </summary>
        /// <param name="colorComponents"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public static TextureFormat ToUnityTextureFormat(this ColorComponents colorComponents)
        {
            switch (colorComponents)
            {
                case ColorComponents.RedGreenBlueAlpha:
                    return TextureFormat.RGBA32;
                
                case ColorComponents.RedGreenBlue:
                    return TextureFormat.RGB24;
                
                case ColorComponents.Grey:
                    return TextureFormat.Alpha8;
                
                case ColorComponents.GreyAlpha:
                case ColorComponents.Default:
                default:
                    throw new NotImplementedException();
            }
        }
    }
}