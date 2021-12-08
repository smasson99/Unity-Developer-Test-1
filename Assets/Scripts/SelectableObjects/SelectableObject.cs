using UnityEngine;

namespace SelectableObjects
{
    public abstract class SelectableObject : MonoBehaviour
    {
        private bool isSelected;
        
        [Header("SelectableObject Settings")]
        
        public SelectableObjectValue SelectedObject;
        public bool IsSelectable = true;

        protected virtual void OnEnable()
        {
            SelectedObject.OnValueChanged += OnSelectedObjectValueChanged;
        }

        protected virtual void OnDisable()
        {
            SelectedObject.OnValueChanged -= OnSelectedObjectValueChanged;
        }

        private void OnSelectedObjectValueChanged(SelectableObject selectableObject)
        {
            if (selectableObject == this)
            {
                isSelected = true;
                OnSelected();
            } 
            else if (isSelected)
            {
                isSelected = false;
                OnDeselected();
            }
        }

        protected abstract void OnSelected();
        protected abstract void OnDeselected();
        public abstract string GetName();
    }
}
