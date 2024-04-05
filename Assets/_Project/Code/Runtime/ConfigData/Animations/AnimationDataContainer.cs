using System.Collections.Generic;
using UnityEngine;

namespace Code.Runtime.ConfigData.Animations
{
    public class AnimationDataContainer : ScriptableObject
    {
        [SerializeField] private List<AnimationData> _animations;
    }
}
