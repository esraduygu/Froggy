using UnityEngine;

namespace Frogger
{
    public class PlayerState : MonoBehaviour
    {
        public PlayerStates state;
        
        public enum PlayerStates
        {
            Idle,
            Leaping,
            Dead
        }
    }
}