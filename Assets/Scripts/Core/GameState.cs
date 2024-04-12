using System;
using UnityEngine;

namespace Core
{
    public class GameState : MonoBehaviour
    {
        public Action<State> OnStateChange;
        public State CurrentState { get; private set; }
        
        public enum State
        {
            StartMenu,
            GetReady,
            Playing,
            GameOver
        }
        
        public void StartMenu()
        {
            UpdateState(State.StartMenu);
        }
        
        public void GameOver()
        {
            UpdateState(State.GameOver);
        }

        public void GetReady()
        {
            UpdateState(State.GetReady);
        }

        public void StartGame()
        {
            UpdateState(State.Playing);
        }

        private void UpdateState(State state)
        {
            CurrentState = state;
            OnStateChange?.Invoke(state);
        }
    }
}
