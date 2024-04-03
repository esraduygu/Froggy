using System;
using Frogger;
using UnityEngine;
using Utilities;

namespace Core
{
    public class ScoreManager : MonoBehaviour
    {
        [SerializeField] private UIManager uiManager;
        [SerializeField] private HomeManager homeManager;
        [SerializeField] private Player player;
        
        private int _score;
        private int _timeLeft;

        // timer based points timer * 20;
        private void OnEnable()
        {
            homeManager.OnAllHomesCleared += AllHomesCleared;
            homeManager.OnHomeCleared += HomeOccupied;
        }

        private void Awake()
        {
            _score = 0;
            uiManager.UpdateScoreText(_score);
            StartCountdown();
        }

        private void AllHomesCleared()
        {
           IncrementScore(1000);
        }
        
        private void HomeOccupied(Home home)
        {
            IncrementScore(50);
        }
        
        private void IncrementScore(int amount)
        {
            _score += amount;
            uiManager.UpdateScoreText(_score);
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
        
        private void OnDisable()
        {
            homeManager.OnAllHomesCleared -= AllHomesCleared;
            homeManager.OnHomeCleared -= HomeOccupied;
        }
    }
}