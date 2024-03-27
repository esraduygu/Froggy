using UnityEngine;

namespace Utilities
{
    public class CameraRunner : MonoBehaviour
    {
        [SerializeField] private Transform player;
        [SerializeField] private Vector3 offset;
        [SerializeField] private float smoothSpeed;
        [SerializeField] private float minYLimit;
        [SerializeField] private float maxYLimit;
        private void Update()
        {
            FollowPlayer();
        }

        private void FollowPlayer()
        {
            var position = player.position;
            var camTransform = transform.position;
        
            var desiredPosition = new Vector3(camTransform.x, position.y, position.z) + offset;
            desiredPosition.y = Mathf.Clamp(desiredPosition.y, minYLimit, maxYLimit);

            var smoothedPosition = Vector3.Lerp(camTransform, desiredPosition, smoothSpeed);
            camTransform = new Vector3(camTransform.x, smoothedPosition.y, smoothedPosition.z);
        
            transform.position = camTransform;
        }
    }
}
