using Code.Runtime.ConfigData.Identifiers;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Code.Runtime.ConfigData.Weapon
{
    [CreateAssetMenu(fileName = "WeaponPlatform", menuName = "StaticData/WeaponPlatform")]
    
    public class WeaponPlatformStaticData : ScriptableObject
    {
        public WeaponId WeaponPlatformId;
        public WeaponData Weapon;
        public AssetReferenceGameObject PrefabReference;
    }

}
