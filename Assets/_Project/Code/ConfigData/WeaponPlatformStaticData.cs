using Code.StaticData.Identifiers;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Code.StaticData
{
    [CreateAssetMenu(fileName = "WeaponPlatform", menuName = "StaticData/WeaponPlatform")]
    
    public class WeaponPlatformStaticData : ScriptableObject
    {
        public WeaponId WeaponPlatformId;
        public WeaponData Weapon;
        public AssetReferenceGameObject PrefabReference;
    }

}
