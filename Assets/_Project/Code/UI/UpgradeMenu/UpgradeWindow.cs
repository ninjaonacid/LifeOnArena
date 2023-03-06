using Code.Infrastructure.Factory;
using Code.Services.PersistentProgress;

namespace Code.UI.UpgradeMenu
{
    public class UpgradeWindow : WindowBase
    {
        private IAbilityFactory _abilityFactory;
        private IHeroFactory _heroFactory;
        public void Construct(
            IAbilityFactory abilityFactory,
            IHeroFactory heroFactory,
            IProgressService progress)
        {
            
            _abilityFactory = abilityFactory;
            _heroFactory = heroFactory;

            var upgradeContainers = GetComponentsInChildren<UpgradeContainer>();

            foreach (var upgradeContainer in upgradeContainers)
            {
                upgradeContainer.Construct(
                    _abilityFactory,
                    _heroFactory);
            }
        }

        protected override void OnAwake()
        {
            base.OnAwake();
        }

    }
}
