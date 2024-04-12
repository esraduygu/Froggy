﻿using System;
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
            gameState.StartMenu();
        }
        
        private void Update()
        {
            if (Input.anyKey) 
                GetReady();
        }
        
        private void GetReady()
        {
            gameState.GetReady();
            _ = new Timer(TimeSpan.FromSeconds(5), gameState.StartGame);
        }
        
    }
}