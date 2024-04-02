using System;
using UnityEngine;

namespace Frogger
{
    public class PlayerState : MonoBehaviour
    {
        public Action<PlayerStates> OnPlayerStateChange;
        
        private PlayerStates _state;
        
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
        
        private void OnStateChange(PlayerStates state)
        {
            _state = state;
            
            OnPlayerStateChange?.Invoke(state);
        }
    }
}