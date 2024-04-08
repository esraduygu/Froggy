using System;
using UnityEngine;

namespace Obstacle
{
    public class Home : MonoBehaviour
    {
        public Action<Home, bool> OnOccupationChange;
        public bool IsOccupied { get; private set; }
        
        [SerializeField] private GameObject homeFrog;
        
        public void SetOccupied(bool occupied)
        {
            IsOccupied = occupied;
            homeFrog.SetActive(occupied);
            
            OnOccupationChange?.Invoke(this, occupied);
        }
    }
}
