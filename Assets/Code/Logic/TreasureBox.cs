using Code.Hero;
using Code.Services;
using UnityEngine;

namespace Code.Logic
{
    public class TreasureBox : MonoBehaviour, IInteractable
    {
        private IGameEventHandler _gameEventHandler;

        private void Awake()
        {
            _gameEventHandler = AllServices.Container.Single<IGameEventHandler>();
        }

        public void Interact(HeroInteraction interactor)
        {
            
        }
    }
}
