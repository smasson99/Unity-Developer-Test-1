using System;
using CameraBehaviours.Behaviours;
using Events;
using UnityEngine;
using Values;

namespace Menus.Brains
{
    [Serializable]
    public struct MenuProfile
    {
        public GameMenuType Type;
        public CameraBehaviourType CameraBehaviourType;
        public BoolValue CanShowMenu;
        public ScriptableEvent ShowEvent;
        public GameObject FirstSelected;
        [Tooltip("If set to true, the cancel UI event will toggle this menu and the previous one.")]
        public bool IsSubMenu;
        [Tooltip("If set to true, the player can controls its camera rotation when displayed.")]
        public bool CanUpdateCameraLook;
        [Tooltip("If set to true, the cancel UI event won't do a thing when displayed.")]
        public bool IsToggleLocked;
        [Tooltip("If set to true, the cancel UI event will toggle this menu with the current one. Only one profile can have this checked.")]
        public bool IsVisibleOnToggle;
    }
}
