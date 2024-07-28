using Code.Runtime.Core.Audio;
using Code.Runtime.Modules.RewardSystem;
using Code.Runtime.UI.Services;
using PrimeTween;
using UnityEngine;
using VContainer;

namespace Code.Runtime.Logic.TreasureChest
{
    public class TreasureChest : MonoBehaviour
    {
        [SerializeField] private LayerMask _player;
        [SerializeField] private GameObject _chestLid;

        private ScreenService _screenService;
        
        private IReward _reward;
        
        public void SetReward(IReward reward)
        {
            _reward = reward;
        }

        public void Open()
        {
            _chestLid.transform.
                DOLocalRotate(new Vector3(5, 0, 0), 1f)
                .SetLink(gameObject)
                .OnComplete(CreateLoot)
                .SetLink(gameObject);
        }

        private void CreateLoot()
        {
            var loot = Instantiate(_reward.LootView, transform);
            loot.LootReady += ClaimReward;
            loot.LootSpawnLogic();
        }

        private void ClaimReward()
        {
            _reward.Claim();
        }
        
    }
}
