using System;
using Core;
using UnityEngine;
using Utilities;

namespace Frogger
{
    public class Player : MonoBehaviour
    {
        public Action OnAdvancedRow;
        
        [SerializeField] private PlayerMovementController movementController;
        [SerializeField] private PlayerInputHandler inputHandler;
        [SerializeField] private PlayerCollisionHandler collisionHandler;
        [SerializeField] private PlayerAnimator animator;
        [SerializeField] private PlayerState playerState;
        [SerializeField] private SfxManager sfxManager;
        
        private Vector3 _initialPos;
        private float _furthestRow;

        private void Awake()
        {
            _initialPos = transform.position;
            _furthestRow = _initialPos.y;
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
            sfxManager.PlaySound(SfxManager.SfxType.Leap);
        }

        private void OnLeapEnd()
        {
            playerState.State = PlayerState.PlayerStates.Idle;
            animator.SetSprite(PlayerAnimator.SpriteType.Idle);
            
            HandleCollisions();
            
            CheckIfAdvancedRow();
        }

        private void CheckIfAdvancedRow()
        {
            if (playerState.State == PlayerState.PlayerStates.Dead ||
                transform.position.y <= _furthestRow) 
                return;
            
            _furthestRow = transform.position.y;
            
            OnAdvancedRow?.Invoke();
        }

        private void OnDirectionInput(Vector2 direction)
        {
            movementController.StartLeap(direction);
        }
        
        private void HandleIdleCollisions()
        {
            if (playerState.State != PlayerState.PlayerStates.Idle || transform.parent != null) 
                return;
            
            HandleObstacleCollision(transform.position);
        }

        private void HandleCollisions()
        {
            var position = transform.position;
            
            if (HandleHomeCollision(position)) 
                return;
            
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
            if (!collisionHandler.CheckObstacle(destination)) 
                return;
            
            transform.position = destination;
            
            Die();
        }

        private bool HandleHomeCollision(Vector3 destination)
        {
            var home = collisionHandler.CheckHome(destination);
            if (home == null) 
                return false;

            home.SetOccupied(true);
            
            Respawn();
            
            return true;
        }
        
        private void Respawn()
        {
            StopAllCoroutines();
            
            movementController.SetPlatform(null);
            transform.SetPositionAndRotation(_initialPos, Quaternion.identity);
            _furthestRow = _initialPos.y;
            animator.SetSprite(PlayerAnimator.SpriteType.Idle);
            inputHandler.enabled = true;
        }

        public void Die()
        {
            movementController.StopAllCoroutines();
            
            animator.SetSprite(PlayerAnimator.SpriteType.Dead);
            sfxManager.PlaySound(SfxManager.SfxType.Dead);
            transform.rotation = Quaternion.identity;
            inputHandler.enabled = false;

            playerState.State = PlayerState.PlayerStates.Dead;
            
            _ = new Timer(TimeSpan.FromSeconds(2), Respawn);
        }

        private void OnDisable()
        {
            movementController.OnLeapStart -= OnLeapStart;
            movementController.OnLeapEnd -= OnLeapEnd;
            inputHandler.OnDirectionInput -= OnDirectionInput;
        }
    }
}

