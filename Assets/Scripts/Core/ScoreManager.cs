using Frogger;
using UnityEngine;

namespace Core
{
    public class ScoreManager : MonoBehaviour
    {
        [SerializeField] private UIManager uiManager;
        [SerializeField] private HomeManager homeManager;
        
        // timer based points timer * 20;

        private int _score;
        private int _time;

        private void OnEnable()
        {
            homeManager.OnAllHomesCleared += AllHomesCleared;
            homeManager.OnHomeCleared += HomeOccupied;
        }

        private void Awake()
        {
            _score = 0;
            uiManager.UpdateScoreText(_score);
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

        private void OnDisable()
        {
            homeManager.OnAllHomesCleared -= AllHomesCleared;
            homeManager.OnHomeCleared -= HomeOccupied;
        }
    }
}