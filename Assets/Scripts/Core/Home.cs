using System;
using System.Linq;
using UnityEngine;

namespace Core
{
    public class Home : MonoBehaviour
    {
        public Action<Home, bool> OnOccupationChange;
        
        [SerializeField] private GameObject homeFrog;

        public bool IsOccupied { get; private set; }
        
        public void SetOccupied(bool occupied)
        {
            IsOccupied = occupied;
            homeFrog.SetActive(occupied);
            
            OnOccupationChange?.Invoke(this, occupied);
        }
    }
}
