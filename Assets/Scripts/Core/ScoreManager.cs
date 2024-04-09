using Frogger;
using Obstacle;
using UI;
using UnityEngine;

namespace Core
{
    public class ScoreManager : MonoBehaviour
    {
        [SerializeField] private GameController gameController;
        [SerializeField] private UIManager uiManager;
        [SerializeField] private SfxManager sfxManager;
        [SerializeField] private HomeManager homeManager;
        [SerializeField] private Player player;
        [SerializeField] private Ticker ticker;

        private int _score;
        private int _bestScore;

        private int BestScore
        {
            get => _bestScore;
            set
            {
                _bestScore = value;
                uiManager.UpdateBestScoreText(value);
            }
        }

        private void OnEnable()
        {
            homeManager.OnAllHomesCleared += AllHomesCleared;
            homeManager.OnHomeCleared += HomeOccupied;
            player.OnAdvancedRow += OnAdvancedRow;
        }

        private void Awake()
        {
            ResetScore();
            LoadBestScore();
        }

        private void LoadBestScore()
        {
            BestScore = PlayerPrefs.GetInt("BestScore");
        }
        
        private void ResetScore()
        {
            _score = 0;
            uiManager.UpdateScoreText(_score);
        }

        private void AllHomesCleared()
        {
            IncrementScore(1000);
            sfxManager.PlaySound(SfxManager.SfxType.Win);
            gameController.NewLevel();
        }
        
        private void HomeOccupied(Home home)
        {
            var remainingTime = ticker.GetTimeLeft();
            var bonusPoints = remainingTime * 20;
            IncrementScore(bonusPoints + 50);
            sfxManager.PlaySound(SfxManager.SfxType.Home);
        }

        private void OnAdvancedRow()
        {
            IncrementScore(10);
        }

        private void IncrementScore(int amount)
        {
            _score += amount;
            
            if (_score > BestScore)
            {            
                BestScore = _score;
                SaveBestScore(_score);
            }

            uiManager.UpdateScoreText(_score);
        }
        
        private static void SaveBestScore(int value)
        {
            PlayerPrefs.SetInt("BestScore", value);
            PlayerPrefs.Save();
        }

        private void OnDisable()
        {
            homeManager.OnAllHomesCleared -= AllHomesCleared;
            homeManager.OnHomeCleared -= HomeOccupied;
            player.OnAdvancedRow -= OnAdvancedRow;
        }
    }
}