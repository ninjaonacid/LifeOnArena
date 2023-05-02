using Code.Hero;
using UnityEngine;

namespace Code.Logic
{
    public interface IInteractable
    {
        void Interact(HeroInteraction interactor);
    }
}
