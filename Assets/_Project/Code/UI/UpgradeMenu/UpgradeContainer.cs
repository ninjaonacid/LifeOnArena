using Code.Data;
using Code.Infrastructure.Factory;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Code.UI.UpgradeMenu
{
    public class UpgradeContainer : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private Image UpgradeImage;
        [SerializeField] private Image UpgradeRarityImage;
        [SerializeField] private TextMeshProUGUI UpgradeText;

        //private PowerUp _powerUp;

        private IAbilityFactory _abilityFactory;
        private CharacterStats _characterStats;
        public void Construct(IAbilityFactory abilityFactory, CharacterStats characterStats)
        {
            _abilityFactory = abilityFactory;
            _characterStats = characterStats;
            SetContainer();
        }

        private void SetContainer()
        {
            //_powerUp = _abilityFactory.GetUpgrade();
            //UpgradeImage.sprite = _powerUp.Icon;
            //UpgradeRarityImage.sprite = _powerUp.PowerUpRarityIcon;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            /*if (_powerUp is Stats statsUp)
            {
                _characterStats.ArmorModifier += statsUp.ArmorModifier;
            }*/
        }
    }
}
