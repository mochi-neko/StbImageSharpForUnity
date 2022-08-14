using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using StbSharp.StbImageSharp;
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
        private IDisposable disposable = null;
        
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
                
                disposable.Dispose();
                disposable = null;
            }

            target.material.mainTexture = null;
        }
        
        private async UniTask DownloadAndStartAnimationAsync()
        {
            ClearTextures();
            
            var client = SingletonHttpClient.Instance;
            
            await UniTask.SwitchToThreadPool();

            var data = await client.GetByteArrayAsync(url);

            var result = AnimatedGifDecoder.DecodeGifImage(data);
            disposable = result.disposable;

            await UniTask.SwitchToMainThread();

            animatedTextures = result.enumerable.ToAnimatedTextures()
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