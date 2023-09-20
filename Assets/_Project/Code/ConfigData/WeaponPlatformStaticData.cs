using Code.ConfigData.Identifiers;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Code.ConfigData
{
    [CreateAssetMenu(fileName = "WeaponPlatform", menuName = "StaticData/WeaponPlatform")]
    
    public class WeaponPlatformStaticData : ScriptableObject
    {
        public WeaponId WeaponPlatformId;
        public WeaponData Weapon;
        public AssetReferenceGameObject PrefabReference;
    }

}
