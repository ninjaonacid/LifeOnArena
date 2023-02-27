using UnityEngine.SceneManagement;
using UnityEngine;
using System;
using System.Collections;

namespace CodeBase.Infrastructure
{
    public partial class GameBootstrapper
    {
        public class SceneLoader
        {
            private readonly ICoroutineRunner _coroutineRunner;

            public SceneLoader(ICoroutineRunner coroutineRunner) => 
                _coroutineRunner = coroutineRunner;

            public void Load(string name, Action onLoaded = null) =>
                _coroutineRunner.StartCoroutine(LoadScene(name, onLoaded));
            
            public IEnumerator LoadScene(string name, Action onLoaded = null)
            {
               AsyncOperation waitNextScene =  SceneManager.LoadSceneAsync(name);

                while (!waitNextScene.isDone)
                    yield return null;

            }
        }

    }
}