using System;
using UnityEngine;

namespace Values
{
    public abstract class ScriptableValue<TValue> : ScriptableObject
    {
        [SerializeField]
        private TValue value;

        public TValue Value
        {
            get => value;
            set
            {
                this.value = value;

                OnValueChanged?.Invoke(this.value);
            }
        }

        public event Action<TValue> OnValueChanged;
    }
}