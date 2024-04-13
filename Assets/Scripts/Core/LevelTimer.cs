using System;
using Frogger;
using TMPro;
using UnityEngine;
using Utilities;

namespace Core
{
    public class LevelTimer : MonoBehaviour
    {
        public int Countdown { get; private set; }

        [SerializeField] private TMP_Text timerText;
        [SerializeField] private Player player;

        private Ticker _ticker;
        private bool _hasStarted;
        
        public void StartCountdown()
        {
            if (_hasStarted) 
                return;
            
            _hasStarted = true;
            
            Countdown = 15;
            UpdateTimerText();
            
            _ticker = new Ticker(TimeSpan.FromSeconds(1), UpdateCountdown);
        }

        private void UpdateCountdown()
        {
            Countdown--;
            UpdateTimerText();

            if (Countdown > 0)
                return;
            
            player.Die();
            StopCountdown();
        }

        private void UpdateTimerText()
        {
            timerText.text = Countdown.ToString();
        }

        public void StopCountdown()
        {
            if (!_hasStarted)
                return;
            
            _hasStarted = false;
            
            _ticker.Dispose();
            _ticker = null;
        }
    }
}