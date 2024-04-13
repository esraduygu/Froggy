using UnityEngine;
using Utilities;

namespace Obstacle
{
    public class Obstacle : MonoBehaviour
    {
        private enum Direction
        {
            Left, 
            Right
        }
        
        [SerializeField] private Direction direction;
        [SerializeField] private float initialSpeed;
        [SerializeField] private float maxGameSpeed;

        private float _speed;

        private void Awake()
        {
            _speed = initialSpeed;
        }

        private void Update()
        {
            Move();
            Destroy();
        }

        private void Move()
        {
            const float gameSpeedIncrease = 0.1f;
            
            _speed += gameSpeedIncrease * Time.deltaTime;
            _speed = Mathf.Clamp(_speed, initialSpeed, maxGameSpeed);
            
            var movementDirection = direction == Direction.Left ? Vector3.left : Vector3.right;
            transform.MoveBy(movementDirection * (_speed * Time.deltaTime));
            Debug.Log($"Speed: {_speed}");
        }

        private void Destroy()
        {
            if (transform.position.x is >= 20 or <= -20)
                Destroy(gameObject);
        }
    }
}
