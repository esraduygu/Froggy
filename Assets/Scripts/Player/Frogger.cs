using System.Collections;
using UnityEngine;
using Utilities;

namespace Player
{
    public class Frogger : MonoBehaviour
    {
        [SerializeField] private float xPos;
        [SerializeField] private float minY;
        [SerializeField] private float maxY;

        private void Update()
        {
            HandleInput();
        }

        private void HandleInput()
        {
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                // transform.MoveBy(Vector3.up);
                // transform.MoveBy(Vector3.up);
                StartLeap(Vector3.up);
                transform.MoveByRotation(new Vector3(0f,0f,0f));
            }
            else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            {
                transform.MoveBy(Vector3.down);
                transform.MoveByRotation(new Vector3(0f,0f,180f));
            }
            else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            {
                transform.MoveBy(Vector3.left);
                transform.MoveByRotation(new Vector3(0f,0f,90f));
            }
            else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            {
                transform.MoveBy(Vector3.right);
                transform.MoveByRotation(new Vector3(0f,0f,-90f));                
            }
        }

        private void LimitPosition()
        {
            var newPosition = transform.position;
            var clampedX = Mathf.Clamp(newPosition.x, -xPos, xPos);
            var clampedY = Mathf.Clamp(newPosition.y, minY, maxY);
            newPosition = new Vector3(clampedX, clampedY, 0);
        
            transform.position = newPosition;
        }

        private void StartLeap(Vector3 direction)
        {
            var destination = transform.position + direction;
            StartCoroutine(Leap(destination));
        }
        
        private IEnumerator Leap(Vector3 destination)
        {
            var startPosition = transform.position;

            var elapsed = 0f;
            var duration = 0.125f;
            
            while (elapsed < duration)
            {
                var normalizedTime = elapsed / duration;
                transform.position = Vector3.Lerp(startPosition, destination, normalizedTime);
                LimitPosition();
                elapsed += Time.deltaTime;
                yield return null;
            }
            transform.position = destination;
            LimitPosition();
        }
    }
}
