using System;
using Code.Runtime.UI.Services;
using Cysharp.Threading.Tasks;
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
            // if (SceneManager.GetActiveScene().name == nextScene)
            // {
            //     onLoaded?.Invoke();
            //     return;
            // }

            _loadingScreen.Show();

            await SceneManager.LoadSceneAsync(nextScene);
            
            onLoaded?.Invoke();

            _loadingScreen.Hide();
        }
        
        
    }
}