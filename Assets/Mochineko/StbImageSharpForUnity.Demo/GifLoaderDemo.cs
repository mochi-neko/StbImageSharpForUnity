using System.Collections.Generic;
using System.IO;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Mochineko.StbImageSharpForUnity.Demo
{
    /// <summary>
    /// A demonstration code of StbImageSharpForUnity loading an GIF image from URL to animated <see cref="Texture2D"/>. 
    /// </summary>
    internal sealed class GifLoaderDemo : MonoBehaviour
    {
        [SerializeField] private string url;
        [SerializeField] private Renderer target;

        private MemoryStream stream = null;
        private CancellationTokenSource cancellationTokenSource = null;

        private void Start()
        {
            DownloadAndStartAnimationAsync().Forget();
        }

        private void OnDestroy()
        {
            Clear();
        }

        private void OnGUI()
        {
            if (GUILayout.Button("Load"))
            {
                DownloadAndStartAnimationAsync().Forget();
            }

            if (GUILayout.Button("Clear"))
            {
                Clear();
            }
        }

        private void Clear()
        {
            cancellationTokenSource?.Cancel();
            cancellationTokenSource = null;
            stream?.Dispose();
            stream = null;
            target.material.mainTexture = null;
        }

        private async UniTask DownloadAndStartAnimationAsync()
        {
            Clear();

            var client = SingletonHttpClient.Instance;

            await UniTask.SwitchToThreadPool();

            var data = await client.GetByteArrayAsync(url);

            stream = new MemoryStream(data);

            await UniTask.SwitchToMainThread();

            cancellationTokenSource = new CancellationTokenSource();

            StartAnimationLoop(stream, cancellationTokenSource.Token).Forget();
        }

        private async UniTask StartAnimationLoop(Stream stream, CancellationToken cancellationToken)
        {
            IEnumerator<TextureSharedAnimatedTexture> enumerator = null;

            await UniTask.SwitchToMainThread(cancellationToken);

            target.material.mainTexture = null;

            while (!cancellationToken.IsCancellationRequested)
            {
                if (enumerator == null)
                {
                    await UniTask.SwitchToThreadPool();

                    // Decode GIF on a thread pool.
                    stream.Seek(offset: 0, origin: 0);
                    enumerator = AnimatedGifDecoder
                        .DecodeGifImage(stream)
                        .ToTextureSharedAnimatedTextures()
                        .GetEnumerator();

                    await UniTask.SwitchToMainThread(cancellationToken);
                }

                // NOTE: Call MoveNext() on the main thread.
                if (enumerator.MoveNext())
                {
                    var frame = enumerator.Current;

                    if (target.material.mainTexture == null)
                    {
                        target.material.mainTexture = frame.Texture;
                    }

                    await UniTask.Delay(frame.DelayInMs, DelayType.DeltaTime, PlayerLoopTiming.Update,
                        cancellationToken);
                }
                else
                {
                    enumerator.Dispose();
                    enumerator = null;
                }
            }

            enumerator?.Dispose();
        }
    }
}