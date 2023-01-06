using Code.Hero;
using Code.Services;
using Code.UI;
using Code.UI.Services;
using UnityEngine;

namespace Code.Logic
{
    public class TreasureBox : MonoBehaviour, IInteractable
    {
        private IGameEventHandler _gameEventHandler;
        private IWindowService _windowService;
        private bool _isActive = false;
        private void Awake()
        {
            _gameEventHandler = AllServices.Container.Single<IGameEventHandler>();
            _windowService = AllServices.Container.Single<IWindowService>();

        }

        
        public void Interact(HeroInteraction interactor)
        {
            _windowService.Open(UIWindowID.UpgradeMenu);
            Debug.Log("Interaction treasure");
        }
    }
}
