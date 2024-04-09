using UnityEngine;
using UnityEngine.UI;
using Utilities;

namespace UI
{
    public class BlinkingUIElement : MonoBehaviour
    {
        [SerializeField] private Graphic graphic;
        [SerializeField] private float blinkingRate;
        
        private void Update()
        {
            Blink();
        }

        private void Blink()
        {
            var alpha= Mathf.Sin(Time.time * blinkingRate) >= 0 ? 1 : 0;
            graphic.color = graphic.color.WithAlpha(alpha);
        }
    }
}