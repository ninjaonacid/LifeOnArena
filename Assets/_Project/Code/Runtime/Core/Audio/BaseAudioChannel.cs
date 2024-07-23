using System;
using Code.Runtime.ConfigData.Audio;
using UnityEngine;
using UnityEngine.Audio;
using Random = UnityEngine.Random;

namespace Code.Runtime.Core.Audio
{
    public abstract class BaseAudioChannel<TAudio> : MonoBehaviour where TAudio : BaseAudioFile
    {
        public AudioSource AudioSource { get; private set; }
        public bool IsFree { get; private set; } = true;

        private AudioMixerGroup _mixerGroup;

        protected TAudio _audioFile;

        public bool IsPlaying => AudioSource.isPlaying;

        private Transform _channelHolder;

        public void SetChannelTransform(Transform targetTransform)
        {
            transform.position = targetTransform.position;
            transform.SetParent(targetTransform);
        }

        public void SetChannelPosition(Vector3 position) => transform.position = position;

        public void InitializeChannel(Transform parent, AudioMixerGroup mixerGroup)
        {
            AudioSource = GetComponent<AudioSource>();
            _channelHolder = parent.transform;
            _mixerGroup = mixerGroup;
            transform.SetParent(parent);
        }

        public void Play(TAudio audioFile)
        {
            _audioFile = audioFile;

            if (!TrySetAudioClip())
            {
                return;
            }

            IsFree = false;

            AudioSource.outputAudioMixerGroup =
                _audioFile.IsMixerGroupOverriden ? _audioFile.AudioMixerGroup : _mixerGroup;

            AudioSource.loop = _audioFile.IsLoopSound;
            AudioSource.Play();
        }

        public void Stop() => AudioSource.Stop();
        public void Pause() => AudioSource.Pause();
        public void UnPause() => AudioSource.UnPause();

        public virtual void Update()
        {
            if (AudioSource != null && !AudioSource.isPlaying)
            {
                IsFree = true;
                transform.SetParent(_channelHolder);
            }
        }

        private bool TrySetAudioClip()
        {
            if (_audioFile.AudioFiles.Count == 0) return false;

            AudioSource.clip = _audioFile.AudioFiles.Count > 1
                ? _audioFile.AudioFiles[Random.Range(0, _audioFile.AudioFiles.Count)]
                : _audioFile.AudioFiles[0];

            if (AudioSource.clip == null)
            {
                Debug.LogError($"Missing audio clip in {_audioFile.name}");
                return false;
            }

            return true;
        }
    }
}