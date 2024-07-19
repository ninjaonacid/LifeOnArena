using Code.Runtime.Modules.RewardSystem;
using Code.Runtime.UI;
using Code.Runtime.UI.Model.DTO;
using Code.Runtime.UI.Services;
using DG.Tweening;
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

        public void Open()
        {
            _isOpened = true;
            _chestLid.transform.DOLocalRotate(new Vector3(5, 0, 0), 1f).SetLink(gameObject);
            var loot = Object.Instantiate(_reward.LootView, transform);
            loot.LootReady += ClaimReward;
            loot.LootSpawnLogic();
        }

        private void ClaimReward()
        {
            _reward.Claim();
        }
        
    }
}
