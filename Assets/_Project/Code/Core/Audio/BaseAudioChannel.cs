using System;
using Code.ConfigData.Audio;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace Code.Core.Audio
{
    public abstract class BaseAudioChannel<TAudio> : MonoBehaviour where TAudio : BaseAudioFile
    {
        public AudioSource AudioSource { get; private set; }
        private AudioMixerGroup _mixerGroup;

        private TAudio _audioFile;
        public bool IsFree;

        public Transform SoundTransformTarget { get; private set; }
        public Transform MainTransformParent { get; private set; }

        public void SetChannelTransform(Transform targetTransform)
        {
            transform.position = targetTransform.position;
            transform.SetParent(targetTransform);
        }

        public void InitializeChannel(Transform parent, AudioMixerGroup mixerGroup)
        {
            AudioSource = GetComponent<AudioSource>();
            IsFree = true;
            MainTransformParent = parent.transform;
            transform.SetParent(parent);
        }
        
        public virtual void Play(TAudio audioFile)
        {
            _audioFile = audioFile;
            
            if (!SetAudioClip())
            {
                return;
            }
            
            IsFree = false;

            AudioSource.outputAudioMixerGroup =
                _audioFile.IsMixerGroupOverriden ? _audioFile.AudioMixerGroup : _mixerGroup;
            
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
