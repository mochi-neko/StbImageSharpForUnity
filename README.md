StbImageSharpForUnity
===

Provides an Unity extension of [StbImageSharp](https://github.com/StbSharp/StbImageSharp).

Decodes a binary image file `byte[]` or `Stream` and converts to [Texture2D](https://docs.unity3d.com/jp/current/ScriptReference/Texture2D-ctor.html) on Unity with pure C# (without any native libraries).

## How to import by UPM

Add

```
dependencies: {
    "com.stbsharp.stbimagesharp": "https://github.com/mochi-neko/StbImageSharpForUnity.git?path=/Assets/StbImageSharp",
    "com.mochineko.stbimagesharp-for-unity": "https://github.com/mochi-neko/StbImageSharpForUnity.git?path=/Assets/Mochineko/StbImageSharpForUnity",
}
```

to `/Packages/manifest.json` on your Unity project and add its reference to your Assembly Definition.

Also you can add a demo codes by adding

```
"com.mochineko.stbimagesharp-for-unity.demo": "https://github.com/mochi-neko/StbImageSharpForUnity.git?path=/Assets/Mochineko/StbImageSharpForUnity.Demo",
```

to your dependencies.


## How to use

A sample usage with the [UniTask](https://github.com/Cysharp/UniTask) is as follows:

```
private async UniTask<Texture2D> LoadImageAsync(byte[] data)
{
    await UniTask.SwitchToThreadPool();

    // Decodes an image on a thread pool.
    var imageResult = ImageDecoder.DecodeImage(data);

    await UniTask.SwitchToMainThread();

    // Creates a texture and set the pixel data on the main thread.
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
- (GIF)
- (HDR)

See also [StbImageSharp](https://github.com/StbSharp/StbImageSharp).

## Support Platforms

All platforms supported by Unity are supported because [StbImageSharp](https://github.com/StbSharp/StbImageSharp) is written by pure C#.

See also [StbImageSharp](https://github.com/StbSharp/StbImageSharp).

## Credits

- [StrImageSharp](https://github.com/StbSharp/StbImageSharp)
- [stb](https://github.com/nothings/stb)

## License

[MIT License](https://github.com/mochi-neko/StbImageSharpForUnity/blob/main/LICENSE)
