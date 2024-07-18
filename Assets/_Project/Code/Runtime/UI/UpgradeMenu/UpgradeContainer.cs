using Code.Runtime.Core.Factory;
using Code.Runtime.Entity.Hero;
using Code.Runtime.Modules.AbilitySystem.PassiveAbilities;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Code.Runtime.UI.UpgradeMenu
{
    public class UpgradeContainer : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private Image _upgradeImage;
        [SerializeField] private Image _upgradeRarityImage;
        [SerializeField] private TextMeshProUGUI _upgradeText;
        [SerializeField] private UpgradeScreen UpgradeScreen;

        private IAbilityFactory _abilityFactory;
      

        private GameObject _hero;
        private PassiveAbilityBlueprintTemplateBase _passiveAbilityBlueprintTemplate;
        public void Construct(IAbilityFactory abilityFactory, 
            IHeroFactory heroFactory)
        {
            _abilityFactory = abilityFactory;

            _hero = heroFactory.HeroGameObject;

            SetContainer();
        }

        private void SetContainer()
        {
            //_passiveAbilityTemplate = _abilityFactory.GetRandomPassiveAbility();
            _upgradeImage.sprite = _passiveAbilityBlueprintTemplate.Icon;
            _upgradeRarityImage.sprite = _passiveAbilityBlueprintTemplate.RarityIcon;
            _upgradeText.text = _passiveAbilityBlueprintTemplate.Description.GetLocalizedString();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            _hero.GetComponent<HeroPassives>().AddPassive(_passiveAbilityBlueprintTemplate);

            UpgradeScreen.CloseButton.onClick.Invoke();
        }
    }
}
