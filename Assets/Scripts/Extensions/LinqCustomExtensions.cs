using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Extensions
{
    public static class LinqCustomExtensions
    {
        public static T RandomElement<T>(this List<T> list)
        {
            return list[list.RandomIndex()];
        }
        
        public static T RandomElement<T>(this List<T> list, int excludedIndex)
        {
            return list[list.RandomIndex(excludedIndex)];
        }

        public static List<T> RandomListOfElements<T>(this List<T> list)
        {
            return RandomListOfElements(list, list.Count);
        }
        
        public static List<T> RandomListOfElements<T>(this List<T> list, int maxAmount)
        {
            var newList = new List<T>();
            var maxIndex = Mathf.Min(list.RandomIndex() + 1, maxAmount);
            
            for (var i = 0; i < maxIndex; ++i)
                newList.Add(list[i]);
            
            return newList;
        }

        public static int RandomIndex<T>(this List<T> list)
        {
            return Random.Range(0, list.Count);
        }
        
        public static int RandomIndex<T>(this List<T> list, int excludedIndex)
        {
            var index = Random.Range(0, list.Count);

            // Debug.Log($"Excluded {excludedIndex} vs {index} {index == excludedIndex && list.Count > 1} {index == excludedIndex} {list.Count}");
            while (index == excludedIndex && list.Count > 1)
                index = Random.Range(0, list.Count);
            // Debug.Log($"Final excluded={excludedIndex} vs result={index}");
            
            return index;
        }
        
        public static T RemoveRandomElement<T>(this List<T> list)
        {
            var element = list[list.RandomIndex()];
            list.Remove(element);
            return element;
        }
        
        public static IEnumerable<T> TakeLast<T>(this IEnumerable<T> source, int lastAmount)
        {
            var enumerable = source as T[] ?? source.ToArray();
            
            return enumerable.Skip(Math.Max(0, enumerable.Count() - lastAmount));
        }
    }
}
