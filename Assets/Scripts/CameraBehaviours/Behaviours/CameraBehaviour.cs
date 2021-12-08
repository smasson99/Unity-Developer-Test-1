using Interfaces;
using UnityEngine;

namespace CameraBehaviours.Behaviours
{
    public abstract class CameraBehaviour : MonoBehaviour, IBehaviour
    {
        protected Camera camera;
        protected float initialFieldOfView;
        protected bool isActive;

        public float FieldOfView = 86;

        private void Awake()
        {
            camera = transform.root.GetComponent<Camera>();
            initialFieldOfView = camera.fieldOfView;
        }

        public abstract void UpdateBehaviour();

        public virtual void OnEnter()
        {
            camera.fieldOfView = FieldOfView;
            isActive = true;
        }

        public virtual void OnExit()
        {
            isActive = false;
            
            if (camera == null)
                return;
            
            camera.fieldOfView = initialFieldOfView;
        }
    }
}
