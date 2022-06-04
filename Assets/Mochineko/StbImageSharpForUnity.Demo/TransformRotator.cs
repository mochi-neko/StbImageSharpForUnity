using UnityEngine;

namespace Mochineko.StbImageSharpForUnity.Demo
{
    // Debugs main thread spiking visually.
    internal sealed class TransformRotator : MonoBehaviour
    {
        [SerializeField] private float speed = 1;
        
        private Transform myTransform;

        private void Awake()
        {
            myTransform = transform;
        }

        private void Update()
        {
            myTransform.Rotate(Vector3.up, speed * Time.deltaTime);
        }
    }
}

