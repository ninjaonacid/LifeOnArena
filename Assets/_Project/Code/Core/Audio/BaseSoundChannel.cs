using UnityEngine;
using UnityEngine.Audio;

namespace Code.Core.Audio
{
    public class BaseSoundChannel : MonoBehaviour
    {
        public AudioSource AudioSource { get; private set; }
        public AudioMixerGroup MixerGroup;

        public bool IsFree;

        public Transform SoundTransform { get; set; }


        public void SetSoundTransform(Transform targetTransform)
        {
            transform.position = targetTransform.position;
        }

    }   
}
