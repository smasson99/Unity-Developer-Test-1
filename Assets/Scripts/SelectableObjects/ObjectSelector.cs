using Layers;
using UnityEngine;
using UnityEngine.InputSystem;

namespace SelectableObjects
{
    public class ObjectSelector : RaycastBrain
    {
        public SelectableObjectValue SelectedObject;

        protected override void OnPerformed(InputAction.CallbackContext context)
        {
            // Debug.Log($"OnSelect");
            var raycastHit = new RaycastHit();

            if (Physics.Raycast
            (
                mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue()),
                out raycastHit,
                ObjectMaxDistance,
                LayerReferences.Singleton.SelectableObjectLayerMask
            ))
            {
                // Debug.Log($"Did hit something");
                var selectedObject = raycastHit.collider.transform.GetComponent<SelectableObject>();

                if (selectedObject == null)
                    selectedObject = raycastHit.collider.transform.root.GetComponent<SelectableObject>();

                if (selectedObject == null)
                    selectedObject = raycastHit.collider.transform.root.GetComponentInChildren<SelectableObject>();

                if (selectedObject != null && !selectedObject.IsSelectable)
                    return;

                // Debug.Log($"SelectableObject is null? {selectedObject == null}");

                // if (selectedObject != null)
                // {
                //     Debug.Log($"name : {selectedObject.gameObject.name}");
                // }

                SelectedObject.Value = null;
                SelectedObject.Value = selectedObject;
            }
            else
            {
                // Debug.Log($"Nothing");
                SelectedObject.Value = null;
            }
        }
    }
}