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
        [SerializeField] private StartMenuScreen startMenuScreen;
        [SerializeField] private GetReadyScreen getReadyMenu;
        [SerializeField] private GameController gameController;

        private void OnEnable()
        {
            gameController.OnStateChange += OnStateChange;
        }

        private void OnStateChange(GameController.GameState state)
        {
            if (state is GameController.GameState.GameOver)
            {
                SetGameOverMenu(true);
                SetGetReadyMenu(false);
            }
            else if (state is GameController.GameState.GetReady)
            {
                SetStartMenu(false);
                SetGetReadyMenu(true);
                SetGameOverMenu(false);
            }
            else if (state is GameController.GameState.Playing)
                SetGetReadyMenu(false);
        }

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

        private void SetGameOverMenu(bool gameOver)
        {
            if (gameOverMenu != null) 
                gameOverMenu.gameObject.SetActive(gameOver);
        }

        private void SetStartMenu(bool startMenu)
        {
            if (startMenuScreen != null)
               startMenuScreen.gameObject.SetActive(startMenu);
        }

        private void SetGetReadyMenu(bool getReady)
        {
            if (getReadyMenu != null)
                getReadyMenu.gameObject.SetActive(getReady);
        }

        private void OnDisable()
        {
            gameController.OnStateChange -= OnStateChange;
        }
    }
}