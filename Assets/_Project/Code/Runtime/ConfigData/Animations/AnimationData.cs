using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Code.Runtime.ConfigData.Animations
{
    [Serializable]
    public class AnimationData
    {
#if UNITY_EDITOR
        [SerializeField]
        [OnValueChanged("OnClipChanged", InvokeOnInitialize = true)]
        [FoldoutGroup("$GetClipName")]
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

#if UNITY_EDITOR
        private void OnClipChanged()
        {
            Length = clip ? clip.length : default;
            Hash = clip ? Animator.StringToHash(clip.name) : default;
        }

        private string GetClipName() =>
            clip ? clip.name : "Empty";
#endif
    }
}
