using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Code.Runtime.ConfigData.Animations
{
    [CreateAssetMenu(fileName = "AnimationDataContainer", menuName = "Config/AnimationContainer")]
    public class AnimationDataContainer : SerializedScriptableObject
    {
        public Dictionary<AnimationKey, AnimationData> Animations;
    }
}
