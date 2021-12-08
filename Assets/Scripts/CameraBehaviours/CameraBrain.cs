using System.Collections.Generic;
using System.Linq;
using CameraBehaviours.Behaviours;
using UnityEngine;

namespace CameraBehaviours
{
    public class CameraBrain : MonoBehaviour
    {
        private CameraBehaviour currentBehaviour;

        public CameraBehaviour CurrentBehaviour
        {
            get { return currentBehaviour; }
            set
            {
                currentBehaviour?.OnExit();
                currentBehaviour = value;
                currentBehaviour?.OnEnter();
            }
        }

        public CameraBehaviourType DefaultBehaviourType;
        public CameraBehaviourTypeValue CurrentBehaviourType;
        public List<CameraBehaviourProfile> Behaviours = new List<CameraBehaviourProfile>();

        private void OnEnable()
        {
            CurrentBehaviourType.OnValueChanged += OnBehaviourTypeChanged;
        }

        private void OnDisable()
        {
            CurrentBehaviourType.OnValueChanged -= OnBehaviourTypeChanged;
        }

        private void SetIfAble(CameraBehaviourType type)
        {
            if (Behaviours.Any(x => x.Type == type))
                CurrentBehaviour = Behaviours.First(x => x.Type == type).Behaviour;
        }

        private void OnBehaviourTypeChanged(CameraBehaviourType type)
        {
            SetIfAble(type);
        }

        private void Start()
        {
            OnBehaviourTypeChanged(CurrentBehaviourType.Value);
            SetIfAble(DefaultBehaviourType);
        }

        private void Update()
        {
            if (CurrentBehaviour == null)
                return;

            CurrentBehaviour.UpdateBehaviour();
        }
    }
}