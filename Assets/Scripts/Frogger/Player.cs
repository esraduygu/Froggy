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
        
        private Vector3 _defaultPos;

        //TODO: When you hit the walls of the homes, you die.
        //TODO: When you hit the homeFrog you die.
        //TODO: Timer
        //TODO: Lives
        //TODO: Scoring
        //TODO: GameState

        private void Awake()
        {
            _defaultPos = transform.position;
        }

        private void OnEnable()
        {
            movementController.OnLeapStart += OnLeapStart;
            movementController.OnLeapEnd += OnLeapEnd;
            inputHandler.OnDirectionInput += OnDirectionInput;
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
            if (HandleHomeCollision(position)) return;
            
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
            Die();
        }

        private bool HandleHomeCollision(Vector3 destination)
        {
            var home = collisionHandler.CheckHome(destination);
            if (home == null) return false;

            home.GetComponent<Home>().enabled = true;
            
            Respawn();
            
            return true;
        }
        
        private void Respawn()
        {
            StopAllCoroutines();
            movementController.SetPlatform(null);
            transform.SetPositionAndRotation(_defaultPos, Quaternion.identity);
            animator.SetSprite(PlayerAnimator.SpriteType.Idle);
            inputHandler.enabled = true;
        }

        private void Die()
        {
            movementController.StopAllCoroutines();
            animator.SetSprite(PlayerAnimator.SpriteType.Dead);
            transform.rotation = Quaternion.identity;
            inputHandler.enabled = false;

            playerState.State = PlayerState.PlayerStates.Dead;
            
            Invoke(nameof(Respawn), 3);
        }

        private void OnDisable()
        {
            movementController.OnLeapStart -= OnLeapStart;
            movementController.OnLeapEnd -= OnLeapEnd;
            inputHandler.OnDirectionInput -= OnDirectionInput;
        }
    }
}

