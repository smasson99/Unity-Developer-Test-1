using Layers;
using Pathfinding;
using SelectableObjects;
using UnityEngine;
using UnityEngine.InputSystem;
using Values;

namespace Units
{
    public class UnitMover : RaycastBrain
    {
        public SelectableObjectValue SelectedObject;
        public Vector3Value MovePoint;
        public GameObject ArrowPrefab;

        private bool HasSelectedUnit => SelectedObject.Value != null && SelectedObject.Value is Unit;

        protected override void OnPerformed(InputAction.CallbackContext context)
        {
            if (!HasSelectedUnit) 
                return;
            
            var raycastHit = new RaycastHit();

            if (!Physics.Raycast
            (
                mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue()),
                out raycastHit,
                ObjectMaxDistance,
                LayerReferences.Singleton.NavigationLayerMask
            )) 
                return;
            
            MovePoint.Value = AstarPath.active.GetNearest(raycastHit.point, NNConstraint.Default).position;
            (SelectedObject.Value as Unit)?.Move(MovePoint.Value);
            Instantiate(ArrowPrefab, MovePoint.Value, Quaternion.identity);
        }
    }
}
