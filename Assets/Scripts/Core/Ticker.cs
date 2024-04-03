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

        private int _timeLeft;

        private void Awake()
        {
            StartCountdown();
        }

        private void StartCountdown()
        {
            _timeLeft = 30;
            uiManager.UpdateTimerText(_timeLeft);
            _ = new Timer(TimeSpan.FromSeconds(1), OnCountdownTick);
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

            _ = new Timer(TimeSpan.FromSeconds(1), OnCountdownTick);
        }

        public int GetTimeLeft()
        {
            return _timeLeft;
        }
    }
}