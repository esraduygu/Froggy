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
        [SerializeField] private float speed;
        
        private void Update()
        {
            Move();
            Destroy();
        }

        private void Move()
        {
            var movementDirection = direction == Direction.Left ? Vector3.left : Vector3.right;
            transform.MoveBy(movementDirection * (speed * Time.deltaTime));
        }

        private void Destroy()
        {
            if (transform.position.x is >= 20 or <= -20)
                Destroy(gameObject);
        }
    }
}
