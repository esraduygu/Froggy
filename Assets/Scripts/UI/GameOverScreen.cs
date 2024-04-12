using System;
using Core;
using UnityEngine;
using Utilities;

namespace UI
{
    public class GameOverScreen : MonoBehaviour
    {
        [SerializeField] private GameState gameState;
        
        private void Update()
        {
            if (Input.anyKeyDown)
                SetGetReady();
        }
        
        private void SetGetReady()
        {
            gameState.GetReady();
            _ = new Timer(TimeSpan.FromSeconds(5), gameState.StartGame);
        }
    }
}