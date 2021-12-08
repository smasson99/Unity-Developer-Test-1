using Michsky.LSS;
using UnityEngine;
using Values;

namespace Menus
{
    public class GameMenu : MonoBehaviour
    {
        private bool lastCanUpdateCameraLookValue;
        private bool lastCanMoveValue;
        private bool lastCanSelectObjectsValue;
        protected bool wasShow;

        [Header("GameMenu Settings")]
        public BoolValue IsVisible;

        [Tooltip("For specific content to disable-enable on Hide and on Show.")]
        public Transform Body;

        public BoolValue CanUpdateCameraLook;
        public BoolValue CanPlayerMove;
        public BoolValue CanSelectObjects;

        public bool IsShowingMouseCursorOnShow;
        public bool IsStopingUpdateCameraViewOnShow;
        public bool IsStopingPlayerMovementsOnShow;
        public bool IsStopingSelectionsOnShow = true;

        public bool IsVisibleAndEnabled => IsVisible.Value && (LoadingScreen.instance == null || LoadingScreen.instance.loadingProcess == null || LoadingScreen.instance.loadingProcess.isDone);

        protected virtual void Awake()
        {
            if (Body == null)
                Body = transform;
        }

        protected virtual void OnEnable()
        {
            IsVisible.OnValueChanged += OnVisibleValueChanged;

            OnVisibleValueChanged(IsVisible.Value);
        }

        protected virtual void OnDisable()
        {
            IsVisible.OnValueChanged -= OnVisibleValueChanged;
        }

        private void OnVisibleValueChanged(bool isVisible)
        {
            if (isVisible)
            {
                Show();
            }
            else
            {
                Hide();
            }
        }

        protected virtual void SetBodyActive(bool isActive)
        {
            if (Body != transform)
                Body.gameObject.SetActive(isActive);
            
            foreach (Transform children in Body)
            {
                if (children.gameObject != null)
                    children.gameObject.SetActive(isActive);
            }
        }

        public virtual void Show()
        {
            SetBodyActive(true);

            Cursor.visible = IsShowingMouseCursorOnShow;
            Cursor.lockState = IsShowingMouseCursorOnShow ? CursorLockMode.None : CursorLockMode.Locked;

            if (IsStopingUpdateCameraViewOnShow)
            {
                lastCanUpdateCameraLookValue = CanUpdateCameraLook.Value;
                CanUpdateCameraLook.Value = false;
            }

            if (IsStopingPlayerMovementsOnShow)
            {
                lastCanMoveValue = CanPlayerMove.Value;
                CanPlayerMove.Value = false;
            }

            if (IsStopingSelectionsOnShow)
            {
                lastCanSelectObjectsValue = CanSelectObjects.Value;
                CanSelectObjects.Value = false;
            }

            wasShow = true;
        }

        public virtual void Hide()
        {
            SetBodyActive(false);

            if (!wasShow)
                return;
            
            if (IsStopingUpdateCameraViewOnShow)
            {
                CanUpdateCameraLook.Value = lastCanUpdateCameraLookValue;
            }

            if (IsStopingPlayerMovementsOnShow)
            {
                CanPlayerMove.Value = lastCanMoveValue;
            }
            
            if (IsStopingSelectionsOnShow)
            {
                CanSelectObjects.Value = lastCanSelectObjectsValue;
            }

            wasShow = false;
        }
    }
}