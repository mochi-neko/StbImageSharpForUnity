StbImageSharpForUnity
===

Provides an Unity extension of [StbImageSharp](https://github.com/StbSharp/StbImageSharp).

Decodes an image file `byte[]` or `Stream` and converts to [Texture2D](https://docs.unity3d.com/jp/current/ScriptReference/Texture2D-ctor.html) on Unity with pure C# (without any native libraries).

## How to import by UPM

Add

```
dependencies: {
    "stbsharp.stbimagesharp": "https://github.com/mochi-neko/StbImageSharpForUnity.git?path=/Assets/StbImageSharp",
    "mochineko.stbimagesharp-for-utity": "https://github.com/mochi-neko/StbImageSharpForUnity.git?path=/Assets/Mochineko/StbImageSharpForUnity",
}
```

to `/Packages/manifest.json` on your Unity project.

Also you can add a demo codes by adding

```
"mochineko.stbimagesharp-for-utity.demo": "https://github.com/mochi-neko/StbImageSharpForUnity.git?path=/Assets/Mochineko/StbImageSharpForUnity.Demo",
```

to dependencies.


## How to use

Default usage with the [UniTask](https://github.com/Cysharp/UniTask) is as follows:

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

All platforms supported by Unity are supported because [StbImageSharp](https://github.com/StbSharp/StbImageSharp) is written by pure C#.

See also [StbImageSharp](https://github.com/StbSharp/StbImageSharp).

## TODO

- GIF extension for Unity
- HDR extension for Unity
- Unsafe API of decoding

## Credits

- [StrImageSharp](https://github.com/StbSharp/StbImageSharp)


## License

[MIT License](https://github.com/mochi-neko/StbImageSharpForUnity/blob/main/LICENSE)
