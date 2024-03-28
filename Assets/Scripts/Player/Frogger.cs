using System.Collections;
using UnityEngine;
using Utilities;

namespace Player
{
    public class Frogger : MonoBehaviour
    {
        [SerializeField] private SpriteManager spriteManager;
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
                StartLeap(Vector3.up);
                transform.SetRotation(new Vector3(0f,0f,0f));
            }
            else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            {
                StartLeap(Vector3.down);
                transform.SetRotation(new Vector3(0f,0f,180f));
            }
            else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            {
                StartLeap(Vector3.left);
                transform.SetRotation(new Vector3(0f,0f,90f));
            }
            else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            {
                StartLeap(Vector3.right);
                transform.SetRotation(new Vector3(0f,0f,-90f));                
            }
        }

        private void SetPosition(Vector3 newPosition)
        {
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
            
            spriteManager.SetSprite(SpriteManager.SpriteType.Leap);
            
            while (elapsed < duration)
            {
                var normalizedTime = elapsed / duration;
                SetPosition(Vector3.Lerp(startPosition, destination, normalizedTime));
                elapsed += Time.deltaTime;
                yield return null;
            }
            
            SetPosition(destination);
            spriteManager.SetSprite(SpriteManager.SpriteType.Idle);
        }
    }
}
