using Frogger;
using Spawn;
using UI;
using UnityEngine;

namespace Core
{
    public class GameController : MonoBehaviour
    {
        [SerializeField] private LivesController livesController;
        [SerializeField] private SpawnManager[] spawnManagers;
        [SerializeField] private ScoreManager scoreManager;
        [SerializeField] private HomeManager homeManager;
        [SerializeField] private LevelTimer levelTimer;
        [SerializeField] private GameState gameState;
        [SerializeField] private UIManager uiManager;
        [SerializeField] private Player player;
        
        private void OnEnable()
        {
            gameState.OnStateChange += OnStateChange;
        }

        private void OnStateChange(GameState.State state)
        {
            UpdatePlayer(state);
            UpdateTicker(state);
            UpdateSpawn(state);
            UpdateLives(state);
            UpdateScore(state);
            UpdateMenus(state);
            UpdateHomes(state);
        }

        private void Update()
        {
            if (gameState.CurrentState is GameState.State.Playing)
            {
                player.HandleIdleCollisions();
                player.enabled = true;
            }
            else
                player.enabled = false;
        }
        
        public void NewLevel()
        {
            homeManager.ResetHomes();

            livesController.UpdateLivesForNewLevel();
            
            player.Respawn();
        }
        
        private void UpdatePlayer(GameState.State state)
        {
            if (state is GameState.State.GameOver)
                player.Respawn();
        }
        
        private void UpdateSpawn(GameState.State state)
        {
            switch (state)
            {
                case GameState.State.Playing or GameState.State.GetReady:
                {
                    foreach (var spawnManager in spawnManagers)
                        spawnManager.StartSpawning();
                    break;
                }
                case GameState.State.StartMenu or GameState.State.GameOver:
                {
                    foreach (var spawnManager in spawnManagers)
                        spawnManager.StopSpawning();
                    break;
                }
            }
        }
        
        private void UpdateTicker(GameState.State state)
        {
            switch (state)
            {
                case GameState.State.GetReady
                    or GameState.State.StartMenu
                    or GameState.State.GameOver:
                    levelTimer.StopCountdown();
                    break;
                case GameState.State.Playing:
                    levelTimer.StartCountdown();
                    break;
            }
        }
        
        private void UpdateLives(GameState.State state)
        {
            if (state is GameState.State.GameOver or GameState.State.GetReady)
                livesController.ResetLives();
        }
        
        private void UpdateScore(GameState.State state)
        {
            if (state is GameState.State.GetReady) 
                scoreManager.ResetScore();
        }
        
        private void UpdateMenus(GameState.State state)
        {
            switch (state)
            {
                case GameState.State.GameOver:
                    uiManager.SetGameOverMenu(true);
                    uiManager.SetGetReadyMenu(false);
                    break;
                case GameState.State.GetReady:
                    uiManager.SetGetReadyMenu(true);
                    uiManager.SetStartMenu(false);
                    uiManager.SetGameOverMenu(false);
                    break;
                case GameState.State.Playing:
                    uiManager.SetGetReadyMenu(false);
                    break;
            }
        }
        
        private void UpdateHomes(GameState.State state)
        {
            if (state is GameState.State.GetReady or GameState.State.StartMenu)
                homeManager.ResetHomes();
        }
        
        private void OnDisable()
        {
            gameState.OnStateChange -= OnStateChange;
        }
    }
}