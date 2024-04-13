using Frogger;
using Obstacle;
using TMPro;
using UnityEngine;

namespace Core
{
    public class ScoreManager : MonoBehaviour
    {
        [SerializeField] private GameController gameController;
        [SerializeField] private HomeManager homeManager;
        [SerializeField] private LevelTimer levelTimer;
        [SerializeField] private SfxManager sfxManager;
        [SerializeField] private TMP_Text scoreText;
        [SerializeField] private TMP_Text bestScoreText;
        [SerializeField] private Player player;

        private int _score;
        private int _bestScore;

        private int BestScore
        {
            get => _bestScore;
            set
            {
                _bestScore = value;
                UpdateScoreText(bestScoreText, value);
            }
        }
        
        private int Score
        {
            get => _score;
            set
            {
                _score = value;
                UpdateScoreText(scoreText, value);
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
        
        public void ResetScore()
        {
            Score = 0;
        }
        
        private void LoadBestScore()
        {
            BestScore = PlayerPrefs.GetInt("BestScore");
        }
        
        private void AllHomesCleared()
        {
            IncrementScore(1000);
            sfxManager.PlaySound(SfxManager.SfxType.Win);
            gameController.NewLevel();
        }
        
        private void HomeOccupied(Home home)
        {
            var remainingTime = levelTimer.Countdown;
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
            Score += amount;
            
            if (Score > BestScore)
            {            
                BestScore = Score;
                SaveBestScore(Score);
            }
        }

        private void UpdateScoreText(TMP_Text text, int value)
        {
            text.text = value.ToString();
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