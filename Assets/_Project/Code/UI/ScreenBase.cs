using Code.Data;
using Code.Services.PersistentProgress;
using UnityEngine;
using UnityEngine.UI;

namespace Code.UI
{
    public class ScreenBase : MonoBehaviour
    {
        public Button CloseButton;
        protected PlayerData Data => _gameDataService.PlayerData;
        
        private IGameDataService _gameDataService;
        public void Construct(IGameDataService gameData)
        {
            _gameDataService = gameData;
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
