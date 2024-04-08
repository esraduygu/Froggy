using System;
using Frogger;
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

        private void OnEnable()
        {
            player.OnDeath += SetLives;
        }

        private void Awake()
        {
            _lives = 3;
            uiManager.UpdateLivesText(_lives);
        }

        private void Update()
        {
            CheckLives();
        }

        private void CheckLives()
        {
            if (_lives > 0)
                return;
            
            _ = new Timer(TimeSpan.FromSeconds(1), gameController.GameOver);
        }

        private void SetLives()
        {
            _lives--;
            uiManager.UpdateLivesText(_lives);
        }
        
        private void OnDisable()
        {
            player.OnDeath -= SetLives;
        }
    }
}