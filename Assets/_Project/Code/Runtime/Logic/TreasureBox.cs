using Code.Runtime.Entity.EntitiesComponents;
using Code.Runtime.Entity.Hero;
using Code.Runtime.UI;
using Code.Runtime.UI.Services;
using UnityEngine;

namespace Code.Runtime.Logic
{
    public class TreasureBox : MonoBehaviour, IInteractable
    {
        private IScreenService _screenService;
        private void Awake()
        {
        }

        
        public void Interact(HeroInteraction interactor)
        {
            _screenService.Open(ScreenID.UpgradeMenu);
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
