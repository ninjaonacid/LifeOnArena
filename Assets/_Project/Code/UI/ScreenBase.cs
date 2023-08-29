using Code.Data;
using Code.Services.PersistentProgress;
using UnityEngine;
using UnityEngine.UI;

namespace Code.UI
{
    public class ScreenBase : MonoBehaviour
    {
        public Button CloseButton;
        protected PlayerData Data => _gameDataContainer.PlayerData;
        
        private IGameDataContainer _gameDataContainer;
        public void Construct(IGameDataContainer gameData)
        {
            _gameDataContainer = gameData;
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
