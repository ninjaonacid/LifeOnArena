using Code.Runtime.Entity.EntitiesComponents;
using Code.Runtime.Entity.Hero;
using Code.Runtime.Modules.RewardSystem;
using Code.Runtime.UI;
using Code.Runtime.UI.Services;
using UnityEngine;

namespace Code.Runtime.Logic
{
    public class TreasureChest : MonoBehaviour, IInteractable
    {
        private ScreenService _screenService;

        private IReward _reward;
        private void Awake()
        {
        }

        
        public void Interact(HeroInteraction interactor)
        {
            _screenService.Open(ScreenID.UpgradeMenu);
            
       
        }
    }
}
