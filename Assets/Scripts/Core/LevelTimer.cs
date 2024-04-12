using System;
using Frogger;
using UI;
using UnityEngine;
using Utilities;

namespace Core
{
    public class LevelTimer : MonoBehaviour
    {
        [SerializeField] private UIManager uiManager;
        [SerializeField] private Player player;

        private Ticker _ticker;
        private int _countdown;
        private bool _hasStarted;
        
        public void StartCountdown()
        {
            if (_hasStarted) return;
            _hasStarted = true;
            
            _countdown = 15;
            uiManager.UpdateTimerText(_countdown);
            
            _ticker = new Ticker(TimeSpan.FromSeconds(1), UpdateCountdown);
        }

        private void UpdateCountdown()
        {
            _countdown--;
            uiManager.UpdateTimerText(_countdown);
            
            if (_countdown <= 0)
            {
                player.Die();
                StopCountdown();
            }
        }

        public void StopCountdown()
        {
            if (!_hasStarted)
                return;
            
            _hasStarted = false;
            
            _ticker.Dispose();
            _ticker = null;
        }
        
        public int GetTimeLeft()
        {
            return _countdown;
        }
    }
}