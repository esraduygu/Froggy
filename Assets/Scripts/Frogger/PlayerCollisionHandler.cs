using UnityEngine;

namespace Frogger
{
    public class PlayerCollisionHandler : MonoBehaviour
    {
        private int _platformLayerMask;
        private int _obstacleLayerMask;
        private int _homeLayerMask;

        private void Awake()
        {
            _platformLayerMask = LayerMask.GetMask("Platform");
            _obstacleLayerMask = LayerMask.GetMask("Obstacle");
            _homeLayerMask = LayerMask.GetMask("Home");
        }
        
        public Collider2D CheckPlatform(Vector3 destination)
        {
            return Physics2D.OverlapBox(destination, Vector2.zero, 0f,
                _platformLayerMask);
        }
        
        public Collider2D CheckHome(Vector3 destination)
        {
            return Physics2D.OverlapBox(destination, Vector2.zero, 0f,
                _homeLayerMask);
        }
        
        public bool CheckObstacle(Vector3 destination)
        {
            var obstacle = Physics2D.OverlapBox(destination, Vector2.zero, 0f,
                _obstacleLayerMask);

            return obstacle != null;
        }
    }
}
