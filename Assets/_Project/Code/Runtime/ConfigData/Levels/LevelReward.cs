using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Code.Runtime.ConfigData.Levels
{
    [CreateAssetMenu(fileName = "LevelReward", menuName = "StaticData/LevelReward")]
    public class LevelReward : ScriptableObject
    {
        public AssetReferenceSprite SpriteReference;
    }
}


