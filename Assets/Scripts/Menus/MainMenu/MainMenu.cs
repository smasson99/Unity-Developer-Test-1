using System;
using Extensions;
using UnityEngine;
using UnityEngine.SceneManagement;
using Values;

namespace Menus.MainMenu
{
    public class MainMenu : GameMenu
    {
        private const int MinNameLength = 2;
        public StringValue PlayerName;
        public SceneProfile MainMenuScene;
        public SceneProfile Scene;
        public GameObject ErrorText;

        private bool IsNameValid => !string.IsNullOrEmpty(PlayerName.Value) && PlayerName.Value.Length >= MinNameLength;

        protected override void Awake()
        {
            base.Awake();
            
            if (SceneManager.GetActiveScene().name == MainMenuScene.name)
                PlayerName.Value = string.Empty;
        }

        public override void Show()
        {
            base.Show();
            
            ErrorText.SetActive(false);
        }

        public void OnNameChanged(string text)
        {
            if (!IsVisible.Value)
                return;

            PlayerName.Value = text;
            ErrorText.SetActive(false);
        }

        public void OnSubmitName()
        {
            OnPlayButtonClicked();
        }

        public void OnPlayButtonClicked()
        {
            if (!IsNameValid)
            {
                ErrorText.SetActive(true);
                return;
            }

            ErrorText.SetActive(false);
            LoadingManagerExtension.Instance.Load(Scene.name);
        }

        public void OnQuitButtonClicked()
        {
            Application.Quit();
        }
    }
}