using System;
using UnityEngine;

namespace Core
{
    public class GameState : MonoBehaviour
    {
        public Action<State> OnStateChange;
        private State _currentState;

        public State CurrentState
        {
            get => _currentState;
            set
            {
                _currentState = value;
                OnStateChange?.Invoke(value);
            }
        }

        public enum State
        {
            StartMenu,
            GetReady,
            Playing,
            GameOver
        }
    }
}