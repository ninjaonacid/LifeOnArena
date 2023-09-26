using System;
using Code.ConfigData.Audio;
using UnityEngine;
using UnityEngine.Audio;
using Random = UnityEngine.Random;

namespace Code.Core.Audio
{
    public abstract class BaseAudioChannel<TAudio> : MonoBehaviour where TAudio : BaseAudioFile
    {
        public AudioSource AudioSource { get; private set; }
        public AudioMixerGroup MixerGroup;

        private TAudio _baseAudioFile;
        public bool IsFree = true;

        public Transform SoundTransform { get; set; }


        public void SetSoundTransform(Transform targetTransform)
        {
            transform.position = targetTransform.position;
        }


        public virtual void Play(TAudio baseAudioFile)
        {
            _baseAudioFile = baseAudioFile;
            IsFree = false;
        }

        public virtual void Update()
        {
            if (!AudioSource.isPlaying)
            {
                IsFree = true;
            }
        }
        private bool SetAudioClip()
        {
            if (_baseAudioFile.AudioFiles.Count > 1)
            {
                int index = Random.Range(0, _baseAudioFile.AudioFiles.Count);
                AudioSource.clip = _baseAudioFile.AudioFiles[index];
                if (AudioSource.clip == null)
                {
                    throw new Exception("Missing audio clip at index " + index);
                }

                return true;
            }
            else if (_baseAudioFile.AudioFiles.Count == 1)
            {
                AudioSource.clip = _baseAudioFile.AudioFiles[0];
                return true;
            }

            return false;
        }

    }   
}
