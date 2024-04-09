using UnityEngine;

namespace Utilities
{
    public static class ColorUtilities
    {
        public static Color WithAlpha(this Color color, float alpha)
        {
            color.a = alpha;
            return color;
        }
    }
}