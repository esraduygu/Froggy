using UnityEngine;

namespace Utilities
{
    public static class BehaviorUtilities 
    {
        public static void Move(Transform transform, Vector3 direction)
        {
            transform.position += direction;
        }
        
        public static void MoveBy(this Transform transform, Vector3 distance)
        {
            transform.position += distance;
        }
    }
}
