using System;
using Units;
using UnityEngine;

namespace Turrets
{
    public class UnitSensor : MonoBehaviour
    {
        public event Action<Unit> OnUnitFound;
        public event Action<Unit> OnUnitLoss;
    
        private void OnTriggerEnter(Collider other)
        {
            var unit = other.GetComponent<Unit>();

            if (unit == null)
                return;
        
            OnUnitFound?.Invoke(unit);
        }
        
        private void OnTriggerExit(Collider other)
        {
            var unit = other.GetComponent<Unit>();

            if (unit == null)
                return;
        
            OnUnitLoss?.Invoke(unit);
        }
    }
}
