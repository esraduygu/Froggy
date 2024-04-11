using System;
using Frogger;
using UI;
using UnityEngine;
using Utilities;

namespace Core
{
    public class Ticker : MonoBehaviour
    {
        [SerializeField] private Player player;
        [SerializeField] private UIManager uiManager;
        [SerializeField] private GameController gameController;

        private Timer _timer;
        private int _timeLeft;
        
        private void Awake()
        {
            StopCountdown();
        }

        private void OnEnable()
        {
            gameController.OnStateChange += OnStateChange;
        }

        private void OnStateChange(GameController.GameState state)
        {
            switch (gameController.State)
            {
                case GameController.GameState.GetReady 
                    or GameController.GameState.StartMenu
                    or GameController.GameState.GameOver:
                    StopCountdown();
                    break;
                default:
                    StartCountdown();
                    break;
            }
        }

        public void StartCountdown()
        {
            _timeLeft = 30;
            uiManager.UpdateTimerText(_timeLeft);
            _timer = new Timer(TimeSpan.FromSeconds(1), OnCountdownTick);
        }

        private void OnCountdownTick()
        {
            _timeLeft--;
            uiManager.UpdateTimerText(_timeLeft);

            if (_timeLeft <= 0)
            {
                player.Die();
                return;
            }

            _timer = new Timer(TimeSpan.FromSeconds(1), OnCountdownTick);
        }
        
        public void StopCountdown()
        {
           _timer?.Dispose();
        }

        public void CancelCountdown()
        {
            _timer?.Cancel();
        }
        
        public int GetTimeLeft()
        {
            return _timeLeft;
        }

        private void OnDisable()
        {
            gameController.OnStateChange -= OnStateChange;
        }
    }
}