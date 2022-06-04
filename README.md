StbImageSharpForUnity
===

Unity extension of StbImageSharp.

## Summary

Decodes image and converts [Texture2D](https://docs.unity3d.com/jp/current/ScriptReference/Texture2D-ctor.html) from `byte[]` or `Stream` on Unity by using [StbImageSharp](https://github.com/StbSharp/StbImageSharp).

## How to import by UPM

Add

```
dependencies: {
    "stbsharp.stbimagesharp": "https://github.com/mochi-neko/StbImageSharpForUnity.git?path=/Assets/StbImageSharp",
    "mochineko.stbimagesharp-for-utity": "https://github.com/mochi-neko/StbImageSharpForUnity.git?path=/Assets/Mochineko/StbImageSharpForUnity",
}
```

to `/Packages/manifest.json` on your Unity project.

If you refer demo, additionally add

```
"mochineko.stbimagesharp-for-utity.demo": "https://github.com/mochi-neko/StbImageSharpForUnity.git?path=/Assets/Mochineko/StbImageSharpForUnity.Demo",
```

to dependencies.


## How to use

Write after.

## Support Codecs

- JPG
- PNG
- BMP
- TGA
- PSD
- GIF
- HDR

Refers [the original README](https://github.com/StbSharp/StbImageSharp).

## Support Platforms

All platforms supported by Unity because StbImageSharp does not use native libraries.

Refers [the original README](https://github.com/StbSharp/StbImageSharp).

## TODO

- GIF extension on Unity

## Credits

- [StrImageSharp](https://github.com/StbSharp/StbImageSharp)


## License

[MIT License](https://github.com/mochi-neko/StbImageSharpForUnity/blob/main/LICENSE)