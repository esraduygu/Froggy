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
            movementController.OnLeapStart += OnLeapStart;
            movementController.OnLeapEnd += OnLeapEnd;
            inputHandler.OnDirectionInput += OnDirectionInput; 
            playerState.OnPlayerStateChange += OnPlayerStateChange;
        }

        private void Update()
        {
            HandleIdleCollisions();
        }
        
        private void OnLeapStart()
        {
            playerState.State = PlayerState.PlayerStates.Leaping;
            animator.SetSprite(PlayerAnimator.SpriteType.Leap);
        }

        private void OnLeapEnd()
        {
            playerState.State = PlayerState.PlayerStates.Idle;
            animator.SetSprite(PlayerAnimator.SpriteType.Idle);
            HandleCollisions();
        }

        private void OnDirectionInput(Vector2 direction)
        {
            movementController.StartLeap(direction);
        }
        
        private void HandleIdleCollisions()
        {
            if (playerState.State != PlayerState.PlayerStates.Idle || transform.parent != null) return;
            HandleObstacleCollision(transform.position);
        }

        private void HandleCollisions()
        {
            var position = transform.position;
            var platform = collisionHandler.CheckPlatform(position);
            
            if (platform != null)
                movementController.SetPlatform(platform.transform);
            else
            {
                movementController.SetPlatform(null);
                HandleObstacleCollision(position);
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
            movementController.OnLeapStart -= OnLeapStart;
            movementController.OnLeapEnd -= OnLeapEnd;
            inputHandler.OnDirectionInput -= OnDirectionInput;
            playerState.OnPlayerStateChange -= OnPlayerStateChange;
        }
    }
}

