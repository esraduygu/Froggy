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
        }

        private void OnDirectionInput(Vector2 direction)
        {
            movementController.StartLeap(direction);
        }

        private void OnDisable()
        {
            inputHandler.OnDirectionInput -= OnDirectionInput;
        }
    }
}

