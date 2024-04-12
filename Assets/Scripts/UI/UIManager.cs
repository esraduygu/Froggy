using TMPro;
using UnityEngine;

namespace UI
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private GameOverScreen gameOverMenu;
        [SerializeField] private StartMenuScreen startMenuScreen;
        [SerializeField] private GameObject getReadyMenu;
        [SerializeField] private TMP_Text scoreText;
        [SerializeField] private TMP_Text bestScoreText;
        [SerializeField] private TMP_Text timerText;
        [SerializeField] private TMP_Text getReadySecondsText;
        [SerializeField] private TMP_Text livesText;
        
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

        public void SetGetReadySeconds(int seconds)
        {
            getReadySecondsText.text = seconds.ToString();
        }
        
        public void SetGameOverMenu(bool gameOver)
        {
            if (gameOverMenu != null) 
                gameOverMenu.gameObject.SetActive(gameOver);
        }

        public void SetStartMenu(bool startMenu)
        {
            if (startMenuScreen != null)
               startMenuScreen.gameObject.SetActive(startMenu);
        }

        public void SetGetReadyMenu(bool getReady)
        {
            if (getReadyMenu != null)
                getReadyMenu.gameObject.SetActive(getReady);
        }
    }
}