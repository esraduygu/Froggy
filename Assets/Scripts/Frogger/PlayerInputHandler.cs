using System;
using UnityEngine;

namespace Frogger
{
    public class PlayerInputHandler : MonoBehaviour
    {
        public Action<Vector2> OnDirectionInput;

        private void Update()
        {
            HandleInput();
        }

        private void HandleInput()
        {
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
                OnDirectionInput?.Invoke(Vector2.up);
            else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
                OnDirectionInput?.Invoke(Vector2.down);
            else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
                OnDirectionInput?.Invoke(Vector2.left);
            else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
                OnDirectionInput?.Invoke(Vector2.right);
        }
    }
}