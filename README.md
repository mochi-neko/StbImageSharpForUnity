StbImageSharpForUnity
===

Provides an Unity extension of [StbImageSharp](https://github.com/StbSharp/StbImageSharp).

## Summary

Decodes image and converts to [Texture2D](https://docs.unity3d.com/jp/current/ScriptReference/Texture2D-ctor.html) from `byte[]` or `Stream` on Unity by using [StbImageSharp](https://github.com/StbSharp/StbImageSharp).

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

Default usage with UniTask is as follows:

```
private async UniTask<Texture2D> LoadImageAsync(byte[] data)
{
    await UniTask.SwitchToThreadPool();

    // Decode image on a thread pool.
    var imageResult = ImageDecoder.DecodeImage(data);

    await UniTask.SwitchToMainThread();

    // Create texture and set data on main thread.
    return imageResult.ToTexture2D();
}
```
.

See also [Demo](https://github.com/mochi-neko/StbImageSharpForUnity/blob/main/Assets/Mochineko/StbImageSharpForUnity.Demo/ImageLoaderDemo.cs).

## Support Codecs

- JPG
- PNG
- BMP
- TGA
- PSD
- GIF
- HDR

See also [StbImageSharp](https://github.com/StbSharp/StbImageSharp).

## Support Platforms

All platform supported by Unity because StbImageSharp does not use native libraries.

See also [StbImageSharp](https://github.com/StbSharp/StbImageSharp).

## TODO

- GIF extension for Unity
- HDR extension for Unity
- Unsafe API of decoding

## Credits

- [StrImageSharp](https://github.com/StbSharp/StbImageSharp)


## License

[MIT License](https://github.com/mochi-neko/StbImageSharpForUnity/blob/main/LICENSE)
