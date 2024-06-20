using Code.Runtime.Modules.RewardSystem;
using Code.Runtime.UI;
using Code.Runtime.UI.Model.DTO;
using Code.Runtime.UI.Services;
using UnityEngine;
using VContainer;

namespace Code.Runtime.Logic.TreasureChest
{
    public class TreasureChest : MonoBehaviour
    {
        [SerializeField] private LayerMask _player;
        [SerializeField] private GameObject _chestLid;

        private ScreenService _screenService;
        private bool _isOpened;

        private IReward _reward;
        
        [Inject]
        private void Construct(ScreenService screenService)
        {
            _screenService = screenService;
        }

        public void SetReward(IReward reward)
        {
            _reward = reward;
        }

        private void Open()
        {
            
        }

        public void OnTriggerEnter(Collider other)
        {
            if (_player == 1 << other.gameObject.layer && !_isOpened)
            {
                Open();
                _isOpened = true;
                var loot = Object.Instantiate(_reward.LootView, transform);
                loot.SetTarget(other.transform);
                loot.LootSpawnLogic();
                _reward.Claim();
            }
        }
    }
}
