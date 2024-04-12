using UnityEngine;

namespace Utilities
{
    public class PlatformFollower : MonoBehaviour
    {
        private Transform _platform;
        private Vector3 _lastPlatformPosition;

        public Transform Platform
        {
            get => _platform;
            set
            {
                _platform = value;
                
                if (value != null)
                    _lastPlatformPosition = value.position;
            }
        }

        private void LateUpdate()
        {
            FollowPlatform();
        }

        private void FollowPlatform()
        {
            if (Platform == null)
                return;

            var platformPosition = Platform.position;
            var delta = platformPosition - _lastPlatformPosition;
            transform.position += delta;
            _lastPlatformPosition = platformPosition;
        }
    }
}