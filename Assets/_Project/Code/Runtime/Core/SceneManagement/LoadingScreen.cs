using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Code.Runtime.Core.SceneManagement
{
    public class LoadingScreen : MonoBehaviour, ILoadingScreen
    {
        public CanvasGroup _loadingScreen;
        public Image _progressBar;
        private void Awake()
        {
            DontDestroyOnLoad(this);
        }

        public void Show()
        {
            gameObject.SetActive(true);
            _loadingScreen.alpha = 1;
        }

        public void Hide()
        {
            StartCoroutine(DoFadeIn());
        }

        public void UpdateProgress(float progress)
        {
            _progressBar.fillAmount = progress;
        }

        private IEnumerator DoFadeIn()
        {
            while (_loadingScreen.alpha > 0)
            {
                _loadingScreen.alpha -= 0.03f;
                yield return new WaitForSeconds(0.03f);
            }

            gameObject.SetActive(false);
        }
    }
}