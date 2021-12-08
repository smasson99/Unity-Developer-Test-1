using Menus;
using UnityEngine;
using UnityEngine.InputSystem;
using Values;

namespace SelectableObjects
{
    public abstract class RaycastBrain : MonoBehaviour
    {
        protected const float ObjectMaxDistance = 500f;
        
        protected Camera mainCamera;
        protected GraphicRaycasters graphicRaycasters;
        
        public TransformValue Canvas;
        public BoolValue CanSelectObjects;
        
        public InputActionReference ActionReference;

        protected virtual void Awake()
        {
            mainCamera = Camera.main;
        }

        private void Start()
        {
            graphicRaycasters = Canvas.Value.GetComponent<GraphicRaycasters>();
        }

        private void OnEnable()
        {
            ActionReference.action.Enable();
            ActionReference.action.started += OnActionRaised;
            ActionReference.action.performed += OnActionRaised;
            ActionReference.action.canceled += OnActionRaised;
        }

        private void OnDisable()
        {
            ActionReference.action.started -= OnActionRaised;
            ActionReference.action.performed -= OnActionRaised;
            ActionReference.action.canceled -= OnActionRaised;
            ActionReference.action.Disable();
        }

        private void OnActionRaised(InputAction.CallbackContext context)
        {
            // Debug.Log($"Called on {ActionReference.name} {context.performed} {!graphicRaycasters.IsPointerOnUI()} {CanSelectObjects.Value}");
            if (context.performed && !graphicRaycasters.IsPointerOnUI() && CanSelectObjects.Value)
                OnPerformed(context);
        }

        protected abstract void OnPerformed(InputAction.CallbackContext context);
    }
}
