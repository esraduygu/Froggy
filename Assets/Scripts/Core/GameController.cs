using System;
using Frogger;
using UI;
using UnityEngine;

namespace Core
{
    public class GameController : MonoBehaviour
    {
        public Action<GameState> OnStateChange;
        public GameState State { get; private set; }
        
        [SerializeField] private Player player;
        [SerializeField] private HomeManager homeManager;
        [SerializeField] private LivesController livesController;
        [SerializeField] private ScoreManager scoreManager;
        [SerializeField] private UIManager uiManager;
        
        private float _timer;

        public enum GameState
        {
            StartMenu,
            GetReady,
            Playing,
            GameOver
        }
        
        public void NewLevel()
        {
            homeManager.ResetHomes();

            livesController.UpdateLivesForNewLevel();
            
            player.Respawn();
        }

        public void StartMenu()
        {
            UpdateState(GameState.StartMenu);
        }
        public void GameOver()
        {
            UpdateState(GameState.GameOver);
        }

        public void GetReady()
        {
            UpdateState(GameState.GetReady);
        }

        public void StartGame()
        {
            UpdateState(GameState.Playing);
        }

        private void UpdateState(GameState state)
        {
            State = state;
            OnStateChange?.Invoke(state);

        }
        
        // public void NewGame()
        // {
        //     if (player.enabled)
        //         return;
        //     
        //     uiManager.SetStartMenu(false);
        //     uiManager.SetGetReadyMenu(false);
        //     player.enabled = true;
        //     homeManager.ResetHomes();
        //     scoreManager.ResetScore();
        //     player.Respawn();
        // }
        //
    }
}
