using Frogger;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Core
{
    public class GameController : MonoBehaviour
    {
        
        [SerializeField] private Player player;
        [SerializeField] private HomeManager homeManager;
        [SerializeField] private UIManager uiManager;
        [SerializeField] private Ticker ticker;
        
        public void NewLevel()
        {
            homeManager.ResetHomes();   
            player.Respawn();
        }
        
        public void GameOver()
        {
            uiManager.SetGameOverMenu(true);
            ticker.CancelCountdown();

            if (Input.anyKey) 
                NewGame();
        }
        
        private void NewGame()
        {
            SceneManager.UnloadScene(0);
            SceneManager.LoadScene(0);
        }
    }
}
