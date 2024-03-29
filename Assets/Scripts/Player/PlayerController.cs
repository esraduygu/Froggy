using UnityEngine;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private Frogger frogger;

        private void OnEnable()
        {
            frogger.OnLeap += CheckPlatform;
        }

        private void CheckPlatform()
        {
            var platform = Physics2D.OverlapBox(transform.position, Vector2.zero, 0f, LayerMask.GetMask($"Platform"));

            transform.SetParent(platform != null ? platform.transform : null);
        }
    }
}
