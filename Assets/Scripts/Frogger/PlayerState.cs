using System;
using UnityEngine;

namespace Frogger
{
    public class PlayerState : MonoBehaviour
    {
        public Action<PlayerStates> OnChange;
        
        private PlayerStates _state;
        
        public enum PlayerStates
        {
            Idle,
            Leaping,
            Dead
        }

        public PlayerStates State
        {
            get => _state;
            set
            {
                if (_state == value) return;
                
                _state = value;
                OnChange?.Invoke(value);
            }
        }
    }
}