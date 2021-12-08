using System.Collections.Generic;
using System.Linq;
using CameraBehaviours.Behaviours;
using Devices;
using Events;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Values;

namespace Menus.Brains
{
    public class MenuBrain : MonoBehaviour
    {
        public List<MenuProfile> MenuProfiles = new List<MenuProfile>();
        public List<BoolValue> IsPopupShowed = new List<BoolValue>();

        private EventSystem eventSystem;
        private Selectable lastTarget;

        private int lastMenuSincePaused;
        private int currentMenuIndex;
        public List<int> StackedMenus = new List<int>();

        private int PauseMenuIndex => MenuProfiles.FindIndex(x => x.IsVisibleOnToggle);
        private int StartingMenuIndex => MenuProfiles.FindIndex(x => x.Type == StartingMenu);
        private int CustomStartingMenuIndex => MenuProfiles.FindIndex(x => x.Type == CustomStartingMenu.Value);
        private bool IsPauseMenu => MenuProfiles.FindIndex(x => x.IsVisibleOnToggle) == currentMenuIndex;

        private bool CanShowPauseMenu => !IsPauseMenu &&
                                         StackedMenus.Count == 0 && !(lastMenuSincePaused >= 0 &&
                                                                      MenuProfiles[currentMenuIndex].IsSubMenu &&
                                                                      MenuProfiles[lastMenuSincePaused].IsToggleLocked);

        private bool CanToggleMenu => currentMenuIndex > 0 &&
                                      !MenuProfiles[currentMenuIndex].IsToggleLocked &&
                                      CanControlUI.Value;

        public GameMenuTypeValue CurrentGameMenu;
        public CameraBehaviourTypeValue CameraBehaviourType;
        public BoolValue CanControlUI;
        public BoolValue CanUpdateCameraLook;
        public ScriptableEvent OnCancelUI;
        public GameDeviceTypeValue ActiveDevice;

        [Space(10)]
        public GameMenuType StartingMenu = GameMenuType.CombatMenu;

        public bool IsUsingCustomStartingIndex;
        public GameMenuTypeValue CustomStartingMenu;

        private void Awake()
        {
            eventSystem = FindObjectOfType<EventSystem>();
            lastMenuSincePaused = -1;
            HideAllMenus();
        }

        private void Start()
        {
            CanControlUI.Value = true;

            currentMenuIndex = IsUsingCustomStartingIndex && CustomStartingMenu.Value != GameMenuType.None
                ? CustomStartingMenuIndex
                : StartingMenuIndex;

            ShowMenu(currentMenuIndex);

            CustomStartingMenu.Value = GameMenuType.MainMenu;
        }

        private void OnEnable()
        {
            for (var i = 0; i < MenuProfiles.Count; i++)
            {
                if (MenuProfiles[i].ShowEvent == null)
                    continue;

                var index = i;
                MenuProfiles[i].ShowEvent.OnRaised += () => { OnShowMenuRequested(index); };
            }

            OnCancelUI.OnRaised += OnCancelUIRaised;
        }

        private void OnDisable()
        {
            for (var i = 0; i < MenuProfiles.Count; i++)
            {
                if (MenuProfiles[i].ShowEvent == null)
                    continue;

                var index = i;
                MenuProfiles[i].ShowEvent.OnRaised -= () => { OnShowMenuRequested(index); };
            }

            OnCancelUI.OnRaised -= OnCancelUIRaised;
        }

        private void OnShowMenuRequested(int index)
        {
            if (this == null)
            {
                MenuProfiles[index].ShowEvent.OnRaised -= () => { OnShowMenuRequested(index); };
                return;
            }

            if (!CanControlUI.Value)
                return;

            ShowMenu(index);
        }

        private void Update()
        {
            if (ActiveDevice.Value == GameDeviceType.PC || lastTarget == null || !lastTarget.IsActive() || eventSystem.currentSelectedGameObject != null &&
                eventSystem.currentSelectedGameObject.GetComponent<Selectable>().IsActive())
                return;

            eventSystem.firstSelectedGameObject = lastTarget.gameObject;
            lastTarget.Select();
        }

        private void SelectMenuTarget(MenuProfile profile)
        {
            lastTarget = profile.FirstSelected != null ? profile.FirstSelected.GetComponent<Selectable>() : null;

            if (ActiveDevice.Value == GameDeviceType.PC)
                return;
            
            eventSystem.firstSelectedGameObject = profile.FirstSelected;
            if (lastTarget != null)
                lastTarget.Select();
        }

        private void ShowMenu(int index)
        {
            var profile = MenuProfiles[index];
            SelectMenuTarget(profile);

            if (currentMenuIndex >= 0)
            {
                if (!profile.IsSubMenu && !profile.IsVisibleOnToggle)
                {
                    StackedMenus.Clear();
                    StackedMenus.Add(index);
                }
                else if (!StackedMenus.Contains(index))
                    StackedMenus.Add(index);
            }

            if (profile.IsSubMenu)
            {
                if (!StackedMenus.Contains(index))
                {
                    StackedMenus.Add(index);
                }

                if (currentMenuIndex >= 0 && MenuProfiles[currentMenuIndex].IsToggleLocked)
                    lastMenuSincePaused = currentMenuIndex;
            }

            HideAllMenus();
            
            currentMenuIndex = index;
            CurrentGameMenu.Value = profile.Type;
            CameraBehaviourType.Value = profile.CameraBehaviourType;
            profile.CanShowMenu.Value = true;

            if (profile.CanUpdateCameraLook)
                CanUpdateCameraLook.Value = true;
        }

        private void HideMenu(int index)
        {
            var profile = MenuProfiles[index];

            profile.CanShowMenu.Value = false;
        }

        private void HideAllMenus()
        {
            CurrentGameMenu.Value = GameMenuType.None;

            for (var i = 0; i < MenuProfiles.Count; i++)
                HideMenu(i);

            currentMenuIndex = -1;
        }

        private void OnCancelUIRaised()
        {
            if (!CanToggleMenu || currentMenuIndex < 0 || IsPopupShowed.Any(x => x.Value))
                return;

            ToggleCurrentMenu();
        }

        private void ToggleCurrentMenu()
        {
            if (StackedMenus.Count > 0)
            {
                StackedMenus.RemoveAt(StackedMenus.Count - 1);
            }

            if (CanShowPauseMenu || StackedMenus.Count > 0)
            {
                if (CanShowPauseMenu && lastMenuSincePaused < 0)
                    lastMenuSincePaused = currentMenuIndex;

                ShowMenu(StackedMenus.Count > 0 ? StackedMenus.Last() : PauseMenuIndex);
            }
            else
            {
                ShowMenu(lastMenuSincePaused);
                lastMenuSincePaused = -1;
            }
        }
    }
}