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
            DownloadAndSetImageAsync().Forget();
        }
        
        private void Clear()
        {
            Destroy(texture);
        }
        
        private async UniTask DownloadAndSetImageAsync()
        {
            await UniTask.SwitchToThreadPool();

            var data = await httpClient.GetByteArrayAsync(url);

            var texture = await LoadImageAsync(data);

            target.material.mainTexture = texture;
        }
        
        private async UniTask<Texture2D> LoadImageAsync(byte[] data)
        {
            await UniTask.SwitchToThreadPool();

            var imageResult = ImageDecoder.DecodeImage(data);

            await UniTask.SwitchToMainThread();

            return imageResult.ToTexture2D();
        }
    }
}