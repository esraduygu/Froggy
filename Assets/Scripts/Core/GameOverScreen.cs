using System;
using UnityEngine;

namespace Core
{
    public class GameOverScreen : MonoBehaviour
    {
        [SerializeField] private GameController gameController;

        private void Update()
        {
            if (Input.anyKeyDown)
                gameController.NewGame();
        }
    }
}