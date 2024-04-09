using Core;
using TMPro;
using UnityEngine;

namespace UI
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private TMP_Text scoreText;
        [SerializeField] private TMP_Text bestScoreText;
        [SerializeField] private TMP_Text timerText;
        [SerializeField] private TMP_Text livesText;
        [SerializeField] private GameOverScreen gameOverMenu;
        
        public void UpdateScoreText(int score)
        {
            scoreText.text = score.ToString();
        }

        public void UpdateBestScoreText(int bestScore)
        {
            bestScoreText.text = bestScore.ToString();
        }

        public void UpdateTimerText(int timer)
        {
            timerText.text = timer.ToString();
        }
        
        public void UpdateLivesText(int lives)
        {
            livesText.text = lives.ToString();
        }

        public void SetGameOverMenu(bool gameOver)
        {
            if (gameOverMenu != null) 
                gameOverMenu.gameObject.SetActive(gameOver);
        }
    }
}