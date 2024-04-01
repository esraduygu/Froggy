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
        }

        private void Update()
        {
            CheckObstacle();
        }

        private void CheckPlatform()
        {
            var platform = Physics2D.OverlapBox(playerMovementController.destination,Vector2.zero,0f, _platformLayerMask);
        
            playerMovementController.transform.SetParent(platform != null ? platform.transform : null);
        }

        private void CheckObstacle()
        {
            var obstacle = Physics2D.OverlapBox(playerMovementController.destination,Vector2.zero,0f, _obstacleLayerMask);

            if (obstacle != null)
                playerState.State = PlayerState.PlayerStates.Dead;
            else
            {
                playerState.State = PlayerState.PlayerStates.Alive;
            }

        }
        
        private void OnDisable()
        {
            playerMovementController.OnLeap -= CheckPlatform;
        }
    }
}
