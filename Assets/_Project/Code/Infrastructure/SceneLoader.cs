using System;
using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;
using VContainer.Unity;

namespace Code.Infrastructure
{
    public class SceneLoader
    {
        private readonly LifetimeScope _currentScope;
        public SceneLoader(LifetimeScope currentScope)
        {
            _currentScope = currentScope;
        }
        public async void Load(string name, Action onLoaded = null)
        {
            await LoadScene(name, onLoaded);
        }
        public async UniTask LoadScene(string nextScene, Action onLoaded = null)
        {
            if (SceneManager.GetActiveScene().name == nextScene)
            {
                onLoaded?.Invoke();
                return;
            }


            await SceneManager.LoadSceneAsync(nextScene);
           

            onLoaded?.Invoke();
        }
    }
}