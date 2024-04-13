using UnityEditor;
using UnityEngine;

namespace UI
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private GameOverScreen gameOverMenu;
        [SerializeField] private StartMenuScreen startMenuScreen;
        [SerializeField] private GameObject getReadyMenu;
        
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
        
        public void OnExitButtonClicked()
        {
#if UNITY_EDITOR
            EditorApplication.ExitPlaymode();
#elif UNITY_STANDALONE_WIN
            Application.Quit();
#endif
        }
    }
}