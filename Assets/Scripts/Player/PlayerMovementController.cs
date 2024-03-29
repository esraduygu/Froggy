using System;
using System.Collections;
using UnityEngine;

namespace Player
{
    public class PlayerMovementController : MonoBehaviour
    {
        public Action OnLeap;
        public Vector3 destination;

        [SerializeField] private PlayerAnimator playerAnimator;
        [SerializeField] private float xPos;
        [SerializeField] private float minY;
        [SerializeField] private float maxY;
        
        public void StartLeap(Vector3 direction)
        {
            destination = transform.position + direction;

            OnLeap?.Invoke();
            StartCoroutine(Leap(destination));
        }
        
        private IEnumerator Leap(Vector3 leapDestination)
        {
            var startPosition = transform.position;
            var elapsed = 0f;
            const float duration = 0.125f;
            
            playerAnimator.SetSprite(PlayerAnimator.SpriteType.Leap);
            
            while (elapsed < duration)
            {
                var normalizedTime = elapsed / duration;
                SetPosition(Vector3.Lerp(startPosition, leapDestination, normalizedTime));
                elapsed += Time.deltaTime;
                yield return null;
            }
            
            SetPosition(leapDestination);
            playerAnimator.SetSprite(PlayerAnimator.SpriteType.Idle);
        }
        
        private void SetPosition(Vector3 newPosition)
        {
            var clampedX = Mathf.Clamp(newPosition.x, -xPos, xPos);
            var clampedY = Mathf.Clamp(newPosition.y, minY, maxY);
            newPosition = new Vector3(clampedX, clampedY, 0);
        
            transform.position = newPosition;
        }
    }
}
