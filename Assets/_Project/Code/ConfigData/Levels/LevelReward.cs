using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Code.StaticData.Levels
{
    [CreateAssetMenu(fileName = "LevelReward", menuName = "StaticData/LevelReward")]
    public class LevelReward : ScriptableObject
    {
        public LocationReward LocationReward;
        public AssetReferenceSprite SpriteReference;

    }
}


