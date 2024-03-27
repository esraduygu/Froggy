using UnityEngine;

namespace Utilities
{
    public static class BehaviorUtilities 
    {
        public static void Move(Transform transform, Vector3 direction)
        {
            transform.position += direction;
        }
    }
}
