using System;
using Frogger;
using UnityEngine;
using Utilities;

namespace Core
{
    public class Ticker : MonoBehaviour
    {
        [SerializeField] private UIManager uiManager;
        [SerializeField] private Player player;

        private Timer _timer;
        private int _timeLeft;

        private void Awake()
        {
            StartCountdown();
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
        
        public int GetTimeLeft()
        {
            return _timeLeft;
        }
    }
}