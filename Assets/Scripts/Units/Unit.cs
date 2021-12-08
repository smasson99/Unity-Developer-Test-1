using Pathfinding;
using SelectableObjects;
using UnityEngine;
using Values;

namespace Units
{
    public class Unit : OutlinedSelectableObject
    {
        private RichAI richAI;
        
        public StringValue PlayerName;

        protected override void Awake()
        {
            base.Awake();

            richAI = GetComponent<RichAI>();
        }

        public override string GetName()
        {
            return PlayerName.Value;
        }

        public void Move(Vector3 point)
        {
            richAI.destination = point;
            richAI.SearchPath();
        }
    }
}