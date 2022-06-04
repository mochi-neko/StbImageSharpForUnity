using System.Net.Http;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Mochineko.StbImageSharpForUnity.Demo
{
    /// <summary>
    /// A demonstration code of StbImageSharpForUnity loading an image from URL to <see cref="Texture2D"/>. 
    /// </summary>
    internal sealed class ImageLoaderDemo : MonoBehaviour
    {
        [SerializeField] private string url;
        [SerializeField] private Renderer target;

        private HttpClient httpClient;
        private Texture2D texture;

        private void Awake()
        {
            httpClient = new HttpClient();
        }

        private void OnDestroy()
        {
            httpClient.Dispose();
        }

        private void Start()
        {
            Load();
        }

        private void OnGUI()
        {
            if (GUILayout.Button($"{nameof(Load)}"))
            {
                Load();
            }
            if (GUILayout.Button($"{nameof(Clear)}"))
            {
                Clear();
            }
        }

        private void Load()
        {
            LoadImageAsync().Forget();
        }
        
        private void Clear()
        {
            Destroy(texture);
        }
        
        private async UniTask LoadImageAsync()
        {
            await UniTask.SwitchToThreadPool();

            var data = await httpClient.GetByteArrayAsync(url);

            var imageResult = ImageDecoder.DecodeImage(data);

            await UniTask.SwitchToMainThread();

            texture = imageResult.ToTexture2D();

            target.material.mainTexture = texture;
        }
    }
}