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

        private Texture2D texture = null;
        
        private void OnDestroy()
        {
            ClearTexture();
        }

        private void Start()
        {
            DownloadAndSetImageAsync().Forget();
        }

        private void OnGUI()
        {
            if (GUILayout.Button("Load"))
            {
                DownloadAndSetImageAsync().Forget();
            }

            if (GUILayout.Button("Clear"))
            {
                ClearTexture();
            }
        }

        private void ClearTexture()
        {
            if (texture != null)
            {
                UnityEngine.Object.Destroy(texture);
                texture = null;
                target.material.mainTexture = null;
            }
        }

        private async UniTask DownloadAndSetImageAsync()
        {
            var client = SingletonHttpClient.Instance;
            
            await UniTask.SwitchToThreadPool();

            var data = await client.GetByteArrayAsync(url);

            var imageResult = ImageDecoder.DecodeImage(data);

            await UniTask.SwitchToMainThread();

            texture = imageResult.ToTexture2D();

            target.material.mainTexture = texture;
        }
    }
}