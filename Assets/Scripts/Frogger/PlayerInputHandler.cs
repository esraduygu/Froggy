using UnityEngine;
using Utilities;

namespace Frogger
{
    public class PlayerInputHandler : MonoBehaviour
    {
        [SerializeField] private PlayerMovementController playerMovement;

        private void Update()
        {
            HandleInput();
        }

        private void HandleInput()
        {
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                playerMovement.StartLeap(Vector3.up);
                transform.SetRotation(new Vector3(0f,0f,0f));
            }
            else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            {
                playerMovement.StartLeap(Vector3.down);
                transform.SetRotation(new Vector3(0f,0f,180f));
            }
            else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            {
                playerMovement.StartLeap(Vector3.left);
                transform.SetRotation(new Vector3(0f,0f,90f));
            }
            else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            {
                playerMovement.StartLeap(Vector3.right);
                transform.SetRotation(new Vector3(0f,0f,-90f));                
            }
        }
    }
}