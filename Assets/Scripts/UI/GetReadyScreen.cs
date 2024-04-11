using Core;
using Frogger;
using UnityEngine;

namespace UI
{
    public class GetReadyScreen : MonoBehaviour
    {
        [SerializeField] private GameController gameController;
        [SerializeField] private Player player;

        private void OnEnable()
        {
            gameController.OnStateChange += OnStateChange;
        }

        private void OnStateChange(GameController.GameState state)
        {
            switch (state)
            {
                case GameController.GameState.Playing:
                    player.enabled = true;
                    break;
            }
        }
        
        private void OnDisable()
        {
            gameController.OnStateChange -= OnStateChange;
        }
    }
}