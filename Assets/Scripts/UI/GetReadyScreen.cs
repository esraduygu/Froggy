using System;
using TMPro;
using UnityEngine;
using Utilities;

namespace UI
{
    public class GetReadyScreen : MonoBehaviour
    {
        [SerializeField] private TMP_Text countdownText;

        private Ticker _ticker;
        private int _countdown;

        private void OnEnable()
        {
            StartCountdown();
        }

        private void StartCountdown()
        {
            _countdown = 3;
            countdownText.SetText(_countdown.ToString());
            
            _ticker = new Ticker(TimeSpan.FromSeconds(1), UpdateCountDown);
        }

        private void UpdateCountDown()
        {
            _countdown--;
            countdownText.SetText(_countdown.ToString());
        }

        private void OnDisable()
        {
            if (_ticker == null)
                return;
            
            _ticker?.Dispose();
            _ticker = null;
        }
    }
}