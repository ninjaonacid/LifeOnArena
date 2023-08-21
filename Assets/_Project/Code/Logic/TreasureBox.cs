using Code.Entity.Hero;
using Code.Logic.EntitiesComponents;
using Code.Services;
using Code.UI;
using Code.UI.Services;
using UnityEngine;

namespace Code.Logic
{
    public class TreasureBox : MonoBehaviour, IInteractable
    {
        private ILevelEventHandler _levelEventHandler;
        private IWindowService _windowService;
        private void Awake()
        {
        }

        
        public void Interact(HeroInteraction interactor)
        {
            _windowService.Open(UIWindowID.UpgradeMenu);
            //_levelEventHandler.GetLevelReward();

            //LocationReward reward = _levelEventHandler.GetLevelReward();
            //switch (reward)
            //{
            //    case LocationReward.Heal:
                    
            //        break;

            //    case LocationReward.Souls:
            //        break;

            //}

            Debug.Log("Interaction treasure");
        }
    }
}
