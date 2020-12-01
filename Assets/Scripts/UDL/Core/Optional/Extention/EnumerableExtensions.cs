using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace UDL.Core
{
    public static class EnumerableExtensions
    {
        public static T RandomPick<T>(this List<T> list)
        {
            return list[UnityEngine.Random.Range(0, list.Count)];
        }

        public static T RandomPick<T>(this List<T> list, System.Random random)
        {
            return list[random.Next(list.Count)];
        }

        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> sequence)
        {
            var retArray = sequence.ToArray();

            // go through array, starting at the last-index
            for (var i = retArray.Length - 1; i > 0; i--)
            {
                var swapIndex = UnityEngine.Random.Range(0, i);   // get num between 0 and index
                if (swapIndex == i) continue;           // don't replace with itself
                var temp = retArray[i];                 // get item at index i...
                retArray[i] = retArray[swapIndex];      // set index i to new item
                retArray[swapIndex] = temp;             // place temp-item to swap-slot
            }

            return retArray;
        }
    }
}
