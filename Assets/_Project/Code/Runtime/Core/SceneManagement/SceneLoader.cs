using System;
using Code.Runtime.Logic.Timer;
using Code.Runtime.UI.Services;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Code.Runtime.Core.SceneManagement
{
    public class SceneLoader
    {
        private readonly ILoadingScreen _loadingScreen;
        private readonly ScreenService _screenService;

        public SceneLoader(ILoadingScreen loadingScreen)
        {
            _loadingScreen = loadingScreen;
        }
        public async void Load(string name, Action onLoaded = null)
        {
            await LoadScene(name, onLoaded);
        }

        private async UniTask LoadScene(string nextScene, Action onLoaded = null)
        {
            _loadingScreen.Show();
            
            AsyncOperation loadSceneOperation = SceneManager.LoadSceneAsync(nextScene);

            loadSceneOperation.allowSceneActivation = false;

            while (loadSceneOperation.progress < 0.9f)
            {
                var progress = loadSceneOperation.progress / 0.9f;
                
                _loadingScreen.UpdateProgress(Mathf.Clamp01(progress));
                
                await UniTask.Yield();
                
            }
            
            _loadingScreen.UpdateProgress(1f);
            
            await UniTask.Delay(TimeSpan.FromSeconds(1f));
            
            loadSceneOperation.allowSceneActivation = true;

            await loadSceneOperation;
            
            onLoaded?.Invoke();

            _loadingScreen.Hide();
        }
    }
}