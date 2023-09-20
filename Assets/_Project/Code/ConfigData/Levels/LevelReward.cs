using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Code.ConfigData.Levels
{
    [CreateAssetMenu(fileName = "LevelReward", menuName = "StaticData/LevelReward")]
    public class LevelReward : ScriptableObject
    {
        public LocationReward LocationReward;
        public AssetReferenceSprite SpriteReference;

    }
}


