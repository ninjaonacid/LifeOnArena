using Code.Runtime.Entity.Hero;

namespace Code.Runtime.Entity.EntitiesComponents
{
    public interface IInteractable
    {
        void Interact(HeroInteraction interactor);
    }
}
