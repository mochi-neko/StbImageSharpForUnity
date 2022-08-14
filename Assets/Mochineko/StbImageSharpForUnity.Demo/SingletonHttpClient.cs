using System.Net.Http;
using UnityEngine;

namespace Mochineko.StbImageSharpForUnity.Demo
{
    /// <summary>
    /// Provides a singleton instance of <see cref="HttpClient"/>.
    /// </summary>
    internal sealed class SingletonHttpClient : MonoBehaviour
    {
        private static HttpClient instance = null;
        public static HttpClient Instance
        {
            get
            {
                if (instance == null)
                {
                    var gameObject = new GameObject(nameof(SingletonHttpClient));
                    GameObject.DontDestroyOnLoad(gameObject);
                    gameObject.AddComponent<SingletonHttpClient>();
                    instance = new HttpClient();
                }

                return instance;
            }
        }
        
        private void OnDestroy()
        {
            instance?.Dispose();
        }
    }
}