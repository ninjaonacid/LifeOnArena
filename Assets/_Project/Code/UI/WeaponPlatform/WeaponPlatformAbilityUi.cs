using Code.ConfigData;
using UnityEngine;
using UnityEngine.UI;

namespace Code.UI.WeaponPlatform
{
    public class WeaponPlatformAbilityUi : MonoBehaviour
    {
        private WeaponData _platformWeapon;


        public void Construct(WeaponData platformWeapon)
        {
            _platformWeapon = platformWeapon;

            var image = GetComponent<Image>();
        }
     
    }
}
