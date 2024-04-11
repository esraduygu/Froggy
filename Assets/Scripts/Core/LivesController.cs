using System;
using Frogger;
using UI;
using UnityEngine;
using Utilities;

namespace Core
{
    public class LivesController : MonoBehaviour
    {
        [SerializeField] private UIManager uiManager;
        [SerializeField] private Player player;
        [SerializeField] private GameController gameController;
        
        private int _lives;

        public int Lives
        {
            get => _lives;

            private set
            {
                _lives = value;
                
                if (value < 0)
                {
                    _ = new Timer(TimeSpan.FromSeconds(1), gameController.GameOver);
                
                    return;
                }
                
                uiManager.UpdateLivesText(value);
            }
        }
        

        private void OnEnable()
        {
            player.OnDeath += DecrementLives;
        }

        private void Awake()
        {
            Lives = 0;
        }
        
        public void UpdateLivesForNewLevel()
        {
            Lives += 1;
        }
        
        private void DecrementLives()
        {
            Lives--;
        }

        private void OnDisable()
        {
            player.OnDeath -= DecrementLives;
        }
    }
}