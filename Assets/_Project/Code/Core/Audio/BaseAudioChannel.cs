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

        private TAudio _audioFile;
        public bool IsFree;

        public Transform SoundTransformTarget { get; set; }
        public Transform MainTransformParent { get; set; }

        public void SetSoundTransform(Transform targetTransform)
        {
            transform.position = targetTransform.position;
            transform.SetParent(targetTransform);
        }

        public void InitializeChannel()
        {
            AudioSource = GetComponent<AudioSource>();
            IsFree = true;
            MainTransformParent = transform.parent;

        }
        public virtual void Play(TAudio audioFile)
        {
            _audioFile = audioFile;
            
            if (!SetAudioClip())
            {
                return;
            }
            
            IsFree = false;

            AudioSource.outputAudioMixerGroup = _audioFile.AudioMixerGroup;
            AudioSource.loop = _audioFile.IsLoopSound;
            AudioSource.Play();
        }
        
        public virtual void Update()
        {
            if (!AudioSource.isPlaying)
            {
                IsFree = true;
                transform.SetParent(MainTransformParent);
            }
        }
        private bool SetAudioClip()
        {
            if (_audioFile.AudioFiles.Count > 1)
            {
                int index = Random.Range(0, _audioFile.AudioFiles.Count);
                AudioSource.clip = _audioFile.AudioFiles[index];
                if (AudioSource.clip == null)
                {
                    throw new Exception("Missing audio clip at index " + index);
                }

                return true;
            }
            else if (_audioFile.AudioFiles.Count == 1)
            {
                AudioSource.clip = _audioFile.AudioFiles[0];
                return true;
            }

            return false;
        }

    }   
}
