using System;
using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace Code.Infrastructure
{
    public class SceneLoader
    {
        private readonly ICoroutineRunner _coroutineRunner;

        public SceneLoader(ICoroutineRunner coroutineRunner)
        {
            _coroutineRunner = coroutineRunner;
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
                
            }

            await SceneManager.LoadSceneAsync(nextScene);

            onLoaded?.Invoke();
        }
    }
}