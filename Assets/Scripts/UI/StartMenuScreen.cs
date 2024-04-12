using System;
using Core;
using UnityEngine;
using Utilities;

namespace UI
{
    public class StartMenuScreen : MonoBehaviour
    {
        [SerializeField] private GameState gameState;
        
        private void Awake()
        {
            gameState.CurrentState = GameState.State.StartMenu;
        }
        
        private void Update()
        {
            if (Input.anyKey) 
                GetReady();
        }
        
        private void GetReady()
        {
            gameState.CurrentState = GameState.State.GetReady;
            _ = new Timer(TimeSpan.FromSeconds(3), () => gameState.CurrentState = GameState.State.Playing);
        }
    }
}