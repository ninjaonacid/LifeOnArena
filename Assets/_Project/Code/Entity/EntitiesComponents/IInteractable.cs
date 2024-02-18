using Code.Entity.Hero;

namespace Code.Entity.EntitiesComponents
{
    public interface IInteractable
    {
        void Interact(HeroInteraction interactor);
    }
}
