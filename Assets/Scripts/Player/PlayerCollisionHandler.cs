using UnityEngine;

namespace Player
{
    public class PlayerCollisionHandler : MonoBehaviour
    {
        [SerializeField] private PlayerMovementController playerMovementController;
        private int _layerMask;

        private void Awake()
        {
            _layerMask = LayerMask.GetMask("Platform");
        }

        private void OnEnable()
        {
            playerMovementController.OnLeap += CheckPlatform;
        }
        
        private void CheckPlatform()
        {
            var platform = Physics2D.OverlapBox(playerMovementController.destination,Vector2.zero,0f, _layerMask);
        
            playerMovementController.transform.SetParent(platform != null ? platform.transform : null);
        }
        
        private void OnDisable()
        {
            playerMovementController.OnLeap -= CheckPlatform;
        }
    }
}
