using UnityEngine;

namespace Frogger
{
    public class PlayerCollisionHandler : MonoBehaviour
    {
        [SerializeField] private PlayerMovementController playerMovementController;
        [SerializeField] private PlayerState playerState;

        private int _platformLayerMask;
        private int _obstacleLayerMask;

        private void Awake()
        {
            _platformLayerMask = LayerMask.GetMask("Platform");
            _obstacleLayerMask = LayerMask.GetMask("Obstacle");
        }

        private void OnEnable()
        {
            playerMovementController.OnLeap += CheckPlatform;
            playerMovementController.OnLeap += CheckObstacle;
        }

        private void CheckPlatform()
        {
            var platform = Physics2D.OverlapBox(playerMovementController.destination, Vector2.zero, 0f,
                _platformLayerMask);

            playerMovementController.transform.SetParent(platform != null ? platform.transform : null);
        }

        private void CheckObstacle()
        {
            var obstacle = Physics2D.OverlapBox(playerMovementController.destination, Vector2.zero, 0f,
                _obstacleLayerMask);
            var platform = Physics2D.OverlapBox(playerMovementController.destination, Vector2.zero, 0f,
                _platformLayerMask);

            if (platform != null || obstacle == null) return;
            transform.position = playerMovementController.destination;
            playerState.State = PlayerState.PlayerStates.Dead;
        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            var hitObstacle = other.gameObject.layer == LayerMask.NameToLayer("Obstacle");
            var onPlatform = transform.parent != null;

            if (!enabled || !hitObstacle || onPlatform) return;
            playerState.State = PlayerState.PlayerStates.Dead;
        }

        private void OnDisable()
        {
            playerMovementController.OnLeap -= CheckPlatform;
            playerMovementController.OnLeap -= CheckObstacle;
        }
    }
}
