using Code.Hero;
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
        private bool _isActive = false;
        private void Awake()
        {
            _levelEventHandler = ServiceLocator.Container.Single<ILevelEventHandler>();
            _windowService = ServiceLocator.Container.Single<IWindowService>();

        }

        
        public void Interact(HeroInteraction interactor)
        {
            _windowService.Open(UIWindowID.UpgradeMenu);
            Debug.Log("Interaction treasure");
        }
    }
}
