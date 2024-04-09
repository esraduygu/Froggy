﻿using System;
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
                if (value < 0)
                    return;
                
                _lives = value;
                uiManager.UpdateLivesText(value);
            
                if (value > 0)
                    return;
            
                _ = new Timer(TimeSpan.FromSeconds(1), gameController.GameOver);
            }
        }
        

        private void OnEnable()
        {
            player.OnDeath += DecrementLives;
        }

        private void Awake()
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

        private void OnDisable()
        {
            player.OnDeath -= DecrementLives;
        }

    }
}