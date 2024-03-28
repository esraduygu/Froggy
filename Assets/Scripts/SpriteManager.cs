using System.Collections.Generic;
using UnityEngine;

public class SpriteManager : MonoBehaviour
{
        public enum SpriteType
        {
            Idle,
            Leap,
            Dead
        }

        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private List<Sprite> sprites;

        public void SetSprite(params SpriteType[] spriteTypes)
        {
            foreach (var spriteType in spriteTypes)
            {
                var index = (int)spriteType;
                spriteRenderer.sprite = sprites[index];
            }
        }
}
