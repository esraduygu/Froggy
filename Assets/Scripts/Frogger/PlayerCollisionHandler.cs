using UnityEngine;

namespace Frogger
{
    public class PlayerCollisionHandler : MonoBehaviour
    {
        private int _platformLayerMask;
        private int _obstacleLayerMask;

        private void Awake()
        {
            _platformLayerMask = LayerMask.GetMask("Platform");
            _obstacleLayerMask = LayerMask.GetMask("Obstacle");
        }
        
        public Collider2D CheckPlatform(Vector3 destination)
        {
            return Physics2D.OverlapBox(destination, Vector2.zero, 0f,
                _platformLayerMask);
        }

        public bool CheckObstacle(Vector3 destination)
        {
            var obstacle = Physics2D.OverlapBox(destination, Vector2.zero, 0f,
                _obstacleLayerMask);

            return obstacle != null;
        }
    }
}
