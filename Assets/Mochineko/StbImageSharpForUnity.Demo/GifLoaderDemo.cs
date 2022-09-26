using System.Collections.Generic;
using System.IO;
using System.Linq;
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
        
        private IReadOnlyList<AnimatedTexture> animatedTextures = null;
        private CancellationTokenSource cancellationTokenSource = null;
        
        private void Start()
        {
            DownloadAndStartAnimationAsync().Forget();
        }
        
        private void OnDestroy()
        {
            ClearTextures();
        }

        private void OnGUI()
        {
            if (GUILayout.Button("Load"))
            {
                DownloadAndStartAnimationAsync().Forget();
            }
            if (GUILayout.Button("Clear"))
            {
                ClearTextures();
            }
        }

        private void ClearTextures()
        {
            if (cancellationTokenSource != null)
            {
                cancellationTokenSource.Cancel();
                cancellationTokenSource = null;
                
                foreach (var animatedTexture in animatedTextures)
                {
                    animatedTexture.Dispose();
                }
                animatedTextures = null;
            }

            target.material.mainTexture = null;
        }
        
        private async UniTask DownloadAndStartAnimationAsync()
        {
            ClearTextures();
            
            var client = SingletonHttpClient.Instance;
            
            await UniTask.SwitchToThreadPool();

            var data = await client.GetByteArrayAsync(url);

            using var stream = new MemoryStream(data);

            var result = AnimatedGifDecoder.DecodeGifImage(stream);

            await UniTask.SwitchToMainThread();

            animatedTextures = result.ToAnimatedTextures()
                .OrderBy(frame => frame.DelayInMs)
                .ToList();

            cancellationTokenSource = new CancellationTokenSource();
            StartAnimationLoop(cancellationTokenSource.Token).Forget();
        }

        private async UniTask StartAnimationLoop(CancellationToken cancellationToken)
        {
            var index = 0;
            
            while (!cancellationToken.IsCancellationRequested)
            {
                var frame = animatedTextures[index];
                
                target.material.mainTexture = frame.Texture;

                await UniTask.Delay(frame.DelayInMs, DelayType.DeltaTime, PlayerLoopTiming.Update, cancellationToken);
                
                index++;
                if (index >= animatedTextures.Count)
                {
                    index = 0;
                }
            }
        }
    }
}