using UnityEngine;

namespace Frogger
{
    public class PlayerCollisionHandler : MonoBehaviour
    {
        [SerializeField] private PlayerState player;
        
        private int _platformLayerMask;
        private int _obstacleLayerMask;

        private void Awake()
        {
            _platformLayerMask = LayerMask.GetMask("Platform");
            _obstacleLayerMask = LayerMask.GetMask("Obstacle");
        }
        
        public void CheckPlatform(Vector3 destination)
        {
            var platform = Physics2D.OverlapBox(destination, Vector2.zero, 0f,
                _platformLayerMask);

            transform.SetParent(platform != null ? platform.transform : null);
        }

        public void CheckObstacle(Vector3 destination, PlayerState playerState)
        {
            var obstacle = Physics2D.OverlapBox(destination, Vector2.zero, 0f,
                _obstacleLayerMask);
            var platform = Physics2D.OverlapBox(destination, Vector2.zero, 0f,
                _platformLayerMask);

            if (platform != null || obstacle == null) return;
            transform.position = destination;
            playerState.State = PlayerState.PlayerStates.Dead;
        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            var hitObstacle = other.gameObject.layer == LayerMask.NameToLayer("Obstacle");
            var onPlatform = transform.parent != null;

            if (!enabled || !hitObstacle || onPlatform) return;
            player.State = PlayerState.PlayerStates.Dead;
        }
        
    }
}
