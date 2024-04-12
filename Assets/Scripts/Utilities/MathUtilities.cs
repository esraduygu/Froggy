using System.Linq;
using UnityEngine;

namespace Utilities
{
    public static class MathUtilities
    {
        public static int PickOne(params float[] probabilities)
        {
            var probabilityIndices = probabilities
                .Select((value, index) => new { value, index })
                .ToDictionary(p => p.index, p => p.value)
                .OrderBy(x => x.Value);

            var random = Random.value;
            
            foreach (var (index, probability) in probabilityIndices)
            {
                random -= probability;

                if (random < 0)
                    return index;
            }
            
            return -1;
        }
    }
}
