using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Assertions;

namespace Code.Runtime.ConfigData.Animations
{
    [Serializable]
    public class AnimationData
    {
#if UNITY_EDITOR
        [SerializeField]
        [FoldoutGroup("$GetClipName")]
        [OnValueChanged("GetClipData")]
        [BoxGroup("$GetClipName/Settings", ShowLabel = false)]
        private AnimationClip clip;
#endif
        [field: SerializeField]
        [field: ReadOnly]
        [field: FoldoutGroup("$GetClipName")]
        [field: BoxGroup("$GetClipName/Settings")]
        public float Length { get; private set; }

        [field: SerializeField]
        [field: ReadOnly]
        [field: FoldoutGroup("$GetClipName")]
        [field: BoxGroup("$GetClipName/Settings")]
        public int Hash { get; private set; }

        [field: SerializeField]
        [field: ReadOnly]
        [field: FoldoutGroup("$GetClipName")]
        [field: BoxGroup("$GetClipName/Settings")]
        public string AnimationName;

#if UNITY_EDITOR
        private void GetClipData()
        {
            Length = clip ? clip.length : default;
            AnimationName = clip ? clip.name : default;
            Hash = clip ? Animator.StringToHash(clip.name) : default;
            
            Assert.IsNotNull(clip);
        }

        private string GetClipName() =>
            clip ? clip.name : "Empty";
#endif
    }
}
