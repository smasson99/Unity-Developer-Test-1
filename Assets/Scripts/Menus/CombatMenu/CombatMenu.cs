using SelectableObjects;
using TMPro;
using UnityEngine;

namespace Menus.CombatMenu
{
    public class CombatMenu : GameMenu
    {
        public GameObject NameContainer;
        public TextMeshProUGUI SelectionNameText;

        public SelectableObjectValue Selection;

        protected override void OnEnable()
        {
            base.OnEnable();

            Selection.OnValueChanged += OnSelectionChanged;
        }

        protected override void OnDisable()
        {
            base.OnDisable();

            Selection.OnValueChanged -= OnSelectionChanged;
        }

        private void OnSelectionChanged(SelectableObject selectableObject)
        {
            if (selectableObject == null)
            {
                SelectionNameText.text = string.Empty;
                NameContainer.SetActive(false);
                return;
            }

            SelectionNameText.text = selectableObject.GetName();
            NameContainer.SetActive(true);
        }
        
        public override void Show()
        {
            base.Show();
            
            OnSelectionChanged(Selection.Value);
            CanSelectObjects.Value = true;
            CanUpdateCameraLook.Value = true;
        }
    }
}