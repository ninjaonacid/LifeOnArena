using Code.StaticData;
using UnityEngine;
using UnityEngine.UI;

namespace Code.UI.WeaponPlatform
{
    public class WeaponPlatformAbilityUi : MonoBehaviour
    {
        public WeaponData weaponData;

        private void Awake()
        {
            var image = GetComponent<Image>();
            image.sprite = weaponData.abilityData.SkillIcon;
        }
    }
}
