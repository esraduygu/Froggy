using System;
using Frogger;
using UI;
using UnityEngine;
using Utilities;

namespace Core
{
    public class LevelTimer : MonoBehaviour
    {
        public int Countdown { get; private set; }
        
        [SerializeField] private UIManager uiManager;
        [SerializeField] private Player player;

        private Ticker _ticker;
        private bool _hasStarted;
        
        public void StartCountdown()
        {
            if (_hasStarted) return;
            _hasStarted = true;
            
            Countdown = 15;
            uiManager.UpdateTimerText(Countdown);
            
            _ticker = new Ticker(TimeSpan.FromSeconds(1), UpdateCountdown);
        }

        private void UpdateCountdown()
        {
            Countdown--;
            uiManager.UpdateTimerText(Countdown);

            if (Countdown > 0)
                return;
            
            player.Die();
            StopCountdown();
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