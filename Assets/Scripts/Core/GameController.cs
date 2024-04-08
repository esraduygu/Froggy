using System;
using Frogger;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utilities;

namespace Core
{
    public class GameController : MonoBehaviour
    {
        private Action _onGameOver;
        
        [SerializeField] private HomeManager homeManager;
        [SerializeField] private Player player;
        [SerializeField] private UIManager uiManager;
        [SerializeField] private Ticker ticker;

        private Timer _timer;
        
        public void GameOver()
        {
            uiManager.SetGameOverMenu(true);
            ticker.CancelCountdown();

            if (Input.anyKey) 
                NewGame();
        }
        
        public void NewLevel()
        {
            homeManager.ResetHomes();   
            player.Respawn();
        }
        
        private void NewGame()
        {
            SceneManager.UnloadScene(0);
            SceneManager.LoadScene(0);
        }
    }
}
