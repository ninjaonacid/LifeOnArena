using Code.Entity.Hero;

namespace Code.Logic.EntitiesComponents
{
    public interface IInteractable
    {
        void Interact(HeroInteraction interactor);
    }
}
