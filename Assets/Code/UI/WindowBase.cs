using Code.Data;
using Code.Services.PersistentProgress;
using UnityEngine;
using UnityEngine.UI;

namespace Code.UI
{
    public class WindowBase : MonoBehaviour
    {
        public Button CloseButton;
        protected IPersistentProgressService _progressService;
        protected PlayerProgress Progress => _progressService.Progress;
        public void Construct(IPersistentProgressService persistentProgress)
        {
            _progressService = persistentProgress;

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
