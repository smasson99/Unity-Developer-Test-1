using System;
using Menus;
using Michsky.LSS;
using UnityEngine;
using Values;

namespace Extensions
{
    public class LoadingManagerExtension : MonoBehaviour
    {
        private LoadingScreenManager loadingScreenManager;

        public static LoadingManagerExtension Instance;

        public bool IsLoading => LoadingScreen.instance != null && LoadingScreen.instance.loadingProcess is { isDone: false };
        
        public bool LoadAtStart = false;
        public SceneProfile Scene;

        private void Awake()
        {
            loadingScreenManager = GetComponent<LoadingScreenManager>();

            Instance = this;
        }

        private void Start()
        {
            if (LoadAtStart)
                Load(Scene.name);
        }

        public void Load(string sceneName)
        {
            if (IsLoading)
                return;

            loadingScreenManager.LoadScene(sceneName);
        }
    }
}