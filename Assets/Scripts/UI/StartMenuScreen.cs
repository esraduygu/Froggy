using System;
using Core;
using UnityEngine;
using Utilities;

namespace UI
{
    public class StartMenuScreen : MonoBehaviour
    {
        [SerializeField] private GameController gameController;
        
        private void Awake()
        {
            gameController.StartMenu();
        }
        
        private void Update()
        {
            if (Input.anyKey) 
                GetReady();
        }
        
        private void GetReady()
        {
            gameController.GetReady();
            _ = new Timer(TimeSpan.FromSeconds(3), gameController.StartGame);
        }
    }
}