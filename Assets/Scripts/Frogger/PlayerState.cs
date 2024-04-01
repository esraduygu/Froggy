using UnityEngine;

namespace Frogger
{
    public class PlayerState : MonoBehaviour
    {
        private PlayerStates _state;
        [SerializeField] private PlayerAnimator playerAnimator;
        [SerializeField] private PlayerMovementController playerMovement;
        [SerializeField] private PlayerInputHandler inputHandler;

        public enum PlayerStates
        {
            Alive,
            Dead
        }

        public PlayerStates State
        {
            get => _state;
            set
            {
                if (_state == value) return;
                
                _state = value;
                OnStateChange(value);
            }
        }

        private void OnStateChange(PlayerStates states)
        {
            switch (states)
            {
                case PlayerStates.Dead:
                    playerAnimator.SetSprite(PlayerAnimator.SpriteType.Dead);
                    playerMovement.StopAllCoroutines();
                    playerMovement.transform.rotation = Quaternion.identity;
                    inputHandler.enabled = false;
                    break;
                    
            }
        }
    }
}