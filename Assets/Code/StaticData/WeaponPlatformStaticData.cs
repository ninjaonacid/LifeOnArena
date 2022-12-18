using Code.Logic.ShelterWeapons;
using UnityEngine;


namespace Code.StaticData
{

    [CreateAssetMenu(fileName = "WeaponPlatform", menuName = "StaticData/WeaponPlatform")]
    
    public class WeaponPlatformStaticData : ScriptableObject
    {
  
        public WeaponId WeaponPlatformId;
        public WeaponData Weapon;
        public WeaponPlatform Prefab;

    }

}
