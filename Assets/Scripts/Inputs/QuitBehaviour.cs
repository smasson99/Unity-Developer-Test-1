using UnityEngine;
using UnityEngine.InputSystem;

namespace Inputs
{
    public class QuitBehaviour : MonoBehaviour
    {
        public InputActionReference QuitAction;
        
        private void OnEnable()
        {
            QuitAction.action.Enable();
            QuitAction.action.started += OnActionRaised;
            QuitAction.action.performed += OnActionRaised;
            QuitAction.action.canceled += OnActionRaised;
        }

        private void OnDisable()
        {
            QuitAction.action.started -= OnActionRaised;
            QuitAction.action.performed -= OnActionRaised;
            QuitAction.action.canceled -= OnActionRaised;
            QuitAction.action.Disable();
        }

        private void OnActionRaised(InputAction.CallbackContext context)
        {
            if (context.performed)
                Application.Quit();
        }
    }
}
