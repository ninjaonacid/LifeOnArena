using Code.Data;
using Code.Infrastructure.Factory;
using Code.StaticData.Ability.PassiveAbilities;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Code.UI.UpgradeMenu
{
    public class UpgradeContainer : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private Image _upgradeImage;
        [SerializeField] private Image _upgradeRarityImage;
        [SerializeField] private TextMeshProUGUI _upgradeText;


        private IAbilityFactory _abilityFactory;
        private CharacterStats _characterStats;
        private PassiveAbilityTemplateBase _passiveAbilityTemplate;
        public void Construct(IAbilityFactory abilityFactory, CharacterStats characterStats)
        {
            _abilityFactory = abilityFactory;
            _characterStats = characterStats;
            SetContainer();
        }

        private void SetContainer()
        {
            _passiveAbilityTemplate = _abilityFactory.GetRandomPassiveAbility();
            _upgradeImage.sprite = _passiveAbilityTemplate.Icon;
            _upgradeRarityImage.sprite = _passiveAbilityTemplate.RarityIcon;
            _upgradeText.text = _passiveAbilityTemplate.Description;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            //_passiveAbilityTemplate.GetAbility().Apply();
        }
    }
}
