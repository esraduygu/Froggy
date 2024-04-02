using UnityEngine;

namespace Utilities
{
    public static class BehaviorUtilities
    {

        public static void MoveBy(this Transform transform, Vector3 distance)
        {
            transform.position += distance;
        }
    }
}
