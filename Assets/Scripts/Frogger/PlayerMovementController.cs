using System;
using System.Collections;
using UnityEngine;

namespace Frogger
{
    public class PlayerMovementController : MonoBehaviour
    {
        public Action OnLeapStart;
        public Action OnLeapEnd;

        [SerializeField] private PlayerAnimator animator;
        [SerializeField] private float xPos;
        [SerializeField] private float minY;
        [SerializeField] private float maxY;
        
        public void StartLeap(Vector3 direction)
        {
            var destination = transform.position + direction;

            var rotation = GetRotationForDirection(direction);

            transform.rotation = Quaternion.Euler(0f, 0f, rotation);
            
            StartCoroutine(Leap(destination));
        }
        
        private static float GetRotationForDirection(Vector3 direction)
        {
            if (direction == Vector3.up)
                return 0f;
            if (direction == Vector3.down)
                return 180f;
            if (direction == Vector3.left)
                return 90f;
            if (direction == Vector3.right)
                return -90f;

            return 0;
        }

        private IEnumerator Leap(Vector3 leapDestination)
        {
            var startPosition = transform.position;
            var elapsed = 0f;
            
            const float duration = 0.125f;
            const float cooldownDuration = 0.150f;
            const float animationStart = 0.05f;
            const float animationEnd = 0.1f;
            
            OnLeapStart?.Invoke();
            
            while (elapsed < duration)
            {
                UpdateAnimation(elapsed, animationEnd, animationStart);
                
                var normalizedTime = elapsed / duration;
                SetPosition(Vector3.Lerp(startPosition, leapDestination, normalizedTime));
                elapsed += Time.deltaTime;
                yield return null;
            }
            
            SetPosition(leapDestination);

            while (elapsed < cooldownDuration)
            {
                elapsed += Time.deltaTime;
                yield return null;
            }
            OnLeapEnd?.Invoke();
        }

        private void UpdateAnimation(float elapsed, float animationEnd, float animationStart)
        {
            if (elapsed > animationEnd)
                animator.SetSprite(PlayerAnimator.SpriteType.Idle);
            else if (elapsed > animationStart)
                animator.SetSprite(PlayerAnimator.SpriteType.Leap);
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
