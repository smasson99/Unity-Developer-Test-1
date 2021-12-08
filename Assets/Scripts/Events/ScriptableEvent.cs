using System;
using UnityEngine;

namespace Events
{
    [CreateAssetMenu(menuName = "Game/Event/Event", fileName = "Event")]
    public class ScriptableEvent : ScriptableObject
    {
        public event Action OnRaised;

        public void Raise()
        {
            OnRaised?.Invoke();
        }
    }
}
