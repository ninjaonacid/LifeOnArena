using Code.Infrastructure.Factory;
using Code.Services.PersistentProgress;

namespace Code.UI.UpgradeMenu
{
    public class UpgradeWindow : WindowBase
    {
        private IAbilityFactory _abilityFactory;
        public void Construct(IAbilityFactory abilityFactory, IProgressService progress)
        {
            
            _abilityFactory = abilityFactory;

            var upgradeContainers = GetComponentsInChildren<UpgradeContainer>();
            foreach (var upgradeContainer in upgradeContainers)
            {
                upgradeContainer.Construct(_abilityFactory, progress.Progress.CharacterStats);
            }
        }

        protected override void OnAwake()
        {
            base.OnAwake();
        }

    }
}
