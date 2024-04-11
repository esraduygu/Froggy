using System;
using Core;
using UnityEngine;
using Utilities;

namespace UI
{
    public class GameOverScreen : MonoBehaviour
    {
        [SerializeField] private GameController gameController;
        
        private void Update()
        {
            if (Input.anyKeyDown)
                GetReady();
        }
        
        private void GetReady()
        {
            gameController.GetReady();
            _ = new Timer(TimeSpan.FromSeconds(3), gameController.StartGame);
        }
    }
}