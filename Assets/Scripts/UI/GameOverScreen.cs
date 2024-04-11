using Core;
using UnityEngine;

namespace UI
{
    public class GameOverScreen : MonoBehaviour
    {
        [SerializeField] private GameController gameController;
        
        private void Update()
        {
            if (Input.anyKeyDown)
                gameController.GetReady();
        }
        
     
        
    }
}