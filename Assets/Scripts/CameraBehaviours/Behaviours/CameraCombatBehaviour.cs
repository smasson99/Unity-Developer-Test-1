using UnityEngine;
using Values;

namespace CameraBehaviours.Behaviours
{
    public class CameraCombatBehaviour : CameraBehaviour
    {
        private bool isRigged;
        public BoolValue CanUpdateCameraLook;
        public TransformValue Player;
        public float OffsetY = 3f;
        public float OffsetX = 3f;
        public float OffsetZ = 0f;
        
        public override void UpdateBehaviour()
        {
            if (!CanUpdateCameraLook.Value && camera.transform.parent != null)
                UnRigFromPlayerCamera();
            else if (CanUpdateCameraLook.Value && camera.transform.parent == null)
                RigToPlayerCamera();

            if (isRigged && Player.Value != null)
            {
                camera.transform.position = Player.Value.position + Vector3.up * OffsetY - Vector3.forward * OffsetZ - Vector3.right * OffsetX;
                camera.transform.LookAt(Player.Value);
            }
        }

        public override void OnEnter()
        {
            base.OnEnter();
            
            RigToPlayerCamera();
        }

        private void RigToPlayerCamera()
        {
            isRigged = true;
        }

        public override void OnExit()
        {
            base.OnExit();
            
            UnRigFromPlayerCamera();
        }
        
        private void UnRigFromPlayerCamera()
        {
            isRigged = false;
        }
    }
}
