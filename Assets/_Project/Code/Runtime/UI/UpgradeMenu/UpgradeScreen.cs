using Code.Runtime.Core.Factory;
using Code.Runtime.Services.PersistentProgress;

namespace Code.Runtime.UI.UpgradeMenu
{
    public class UpgradeScreen : ScreenBase
    {
        private AbilityFactory _abilityFactory;
        private HeroFactory _heroFactory;
        public void Construct(
            AbilityFactory abilityFactory,
            HeroFactory heroFactory,
            IGameDataContainer gameData)
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
