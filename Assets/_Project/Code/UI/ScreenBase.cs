using Code.Data;
using Code.Services.PersistentProgress;
using UnityEngine;
using UnityEngine.UI;

namespace Code.UI
{
    public class ScreenBase : MonoBehaviour
    {
        public Button CloseButton;
        protected IProgressService _progressService;
        protected PlayerProgress Progress => _progressService.Progress;
        public void Construct(IProgressService progress)
        {
            _progressService = progress;

        }
        private void Awake()
        {
            OnAwake();
        }

        protected virtual void OnAwake()
        {
            CloseButton.onClick.AddListener(() => Destroy(gameObject));
        }
    }
}
