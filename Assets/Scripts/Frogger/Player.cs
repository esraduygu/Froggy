using UnityEngine;

namespace Frogger
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private PlayerMovementController movementController;
        [SerializeField] private PlayerInputHandler inputHandler;
        [SerializeField] private PlayerCollisionHandler collisionHandler;
        [SerializeField] private PlayerAnimator animator;
        [SerializeField] private PlayerState playerState;

        private void OnEnable()
        {
            inputHandler.OnDirectionInput += OnDirectionInput;
            movementController.OnLeap += OnLeap;
            playerState.OnPlayerStateChange += OnPlayerStateChange;
        }
        
        private void OnDirectionInput(Vector2 direction)
        {
            movementController.StartLeap(direction);
        }

        private void OnLeap()
        {
            HandleCollisions();
        }

        private void Update()
        {
            HandleIdleCollisions();
        }

        private void HandleIdleCollisions()
        {
            if (transform.parent != null) return;
            HandleObstacleCollision(transform.position);
        }

        private void HandleCollisions()
        {
            var destination = movementController.destination;
            var platform = collisionHandler.CheckPlatform(destination);
            
            if (platform != null)
                movementController.SetPlatform(platform.transform);
            else
            {
                movementController.SetPlatform(null);
                HandleObstacleCollision(destination);
            }
        }

        private void HandleObstacleCollision(Vector3 destination)
        {
            if (!collisionHandler.CheckObstacle(destination)) return;
            transform.position = destination;
            playerState.State = PlayerState.PlayerStates.Dead;
        }

        private void OnPlayerStateChange(PlayerState.PlayerStates newState)
        {
            switch (newState)
            {
                case PlayerState.PlayerStates.Dead:
                    movementController.StopAllCoroutines();
                    animator.SetSprite(PlayerAnimator.SpriteType.Dead);
                    movementController.transform.rotation = Quaternion.identity;
                    inputHandler.enabled = false;
                    break;
            }
        }

        private void OnDisable()
        {
            inputHandler.OnDirectionInput -= OnDirectionInput;
            movementController.OnLeap -= OnLeap;
            playerState.OnPlayerStateChange -= OnPlayerStateChange;
        }
    }
}

