﻿using System;
using Code.UI.Services;
using Code.UI.View;
using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace Code.Infrastructure.SceneManagement
{
    public class SceneLoader
    {
        private readonly ILoadingScreen _loadingScreen;
        private readonly IScreenViewService _screenService;

        public SceneLoader(ILoadingScreen loadingScreen, IScreenViewService screenService)
        {
            _loadingScreen = loadingScreen;
            _screenService = screenService;
        }
        public async void Load(string name, Action onLoaded = null)
        {
            await LoadScene(name, onLoaded);
        }

        private async UniTask LoadScene(string nextScene, Action onLoaded = null)
        {
            if (SceneManager.GetActiveScene().name == nextScene)
            {
                onLoaded?.Invoke();
                return;
            }

            _loadingScreen.Show();
            
            _screenService.Cleanup();

            await SceneManager.LoadSceneAsync(nextScene);
            
            onLoaded?.Invoke();

            _loadingScreen.Hide();
        }
    }
}