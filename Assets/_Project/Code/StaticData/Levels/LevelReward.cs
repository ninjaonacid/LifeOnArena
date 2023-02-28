using System;
using System.Collections.Generic;
using UnityEngine.AddressableAssets;

namespace Code.StaticData.Levels
{
    [Serializable]
    public class LevelReward
    {
        public AssetReferenceSprite SpriteReference;
        public LocationReward LocationReward;
    }
}

