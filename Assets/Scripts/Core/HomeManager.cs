using System;
using System.Linq;
using Obstacle;
using UnityEngine;

namespace Core
{
    public class HomeManager : MonoBehaviour
    {
        public Action OnAllHomesCleared;
        public Action<Home> OnHomeCleared;
        
        [SerializeField] private Home[] homes;

        public void ResetHomes()
        {
            foreach (var home in homes) 
                home.SetOccupied(false);
        }
        
        private void OnEnable()
        {
            foreach (var home in homes) 
                home.OnOccupationChange += OnOccupationChange;
        }
        
        private void OnOccupationChange(Home home, bool occupied)
        {
            if (!occupied) 
                return;
            
            CheckAllHomesCleared();
            OnHomeCleared?.Invoke(home);
        }

        private void CheckAllHomesCleared()
        {
            if (homes.All(t => t.IsOccupied))
                OnAllHomesCleared?.Invoke();
        }

        private void OnDisable()
        {
            foreach (var home in homes) 
                home.OnOccupationChange -= OnOccupationChange;
        }
    }
}