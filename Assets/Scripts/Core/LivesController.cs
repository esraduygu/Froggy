using System;
using Frogger;
using TMPro;
using UnityEngine;
using Utilities;

namespace Core
{
    public class LivesController : MonoBehaviour
    {
        [SerializeField] private GameState gameState;
        [SerializeField] private TMP_Text livesText;
        [SerializeField] private Player player;
        
        private int _lives;

        public int Lives
        {
            get => _lives;

            private set
            {
                _lives = value;
                
                if (value <= 0)
                {
                    _ = new Timer(TimeSpan.FromSeconds(1), () => gameState.CurrentState = GameState.State.GameOver);
                
                    return;
                }
                
                UpdateLivesText();
            }
        }
        
        private void OnEnable()
        {
            player.OnDeath += DecrementLives;
        }
        
        private void Awake()
        {
            ResetLives();
        }
        
        public void ResetLives()
        {
            Lives = 3;
        }

        public void UpdateLivesForNewLevel()
        {
            Lives += 1;
        }
        
        private void DecrementLives()
        {
            Lives--;
        }

        private void UpdateLivesText()
        {
            livesText.text = _lives.ToString();
        }

        private void OnDisable()
        {
            player.OnDeath -= DecrementLives;
        }
    }
}