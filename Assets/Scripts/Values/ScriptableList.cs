using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Values
{
    public abstract class ScriptableList<T> : ScriptableObject
    {
        public List<T> List = new List<T>();

        public List<int> Indexes => List.Select((x, i) => i).ToList();
    }
}
