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
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
                OnDirectionInput?.Invoke(Vector2.up);

            else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
                OnDirectionInput?.Invoke(Vector2.down);

            else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
                OnDirectionInput?.Invoke(Vector2.left);

            else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
                OnDirectionInput?.Invoke(Vector2.right);
        }
    }
}