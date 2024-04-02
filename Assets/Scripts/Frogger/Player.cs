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
            collisionHandler.CheckPlatform(movementController.destination);
            collisionHandler.CheckObstacle(movementController.destination, playerState);
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

