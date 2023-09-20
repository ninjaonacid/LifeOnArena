using System;
using Sirenix.Utilities.Editor;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Code.ConfigData.Audio
{
    public enum SoundId
    {
        SwordSlash = 1,
    }
    [Serializable]
    public class Sound
    {
        public string Id;
        public AssetReference SoundRef;
        public AudioSource Source;
        public Transform SourceTransform { get; private set; }
        public bool IsPlaying { get; private set; }
        public bool IsPaused { get; private set; }
        public bool IsStopped { get; private set; }
        public float Volume { get; set; }

        public AudioClip Clip { get; set; }
        
        public bool IsLoopSound;

        public void Initialize(AudioClip clip, AudioSource source, float volume, bool isLoop)
        {
            Clip = clip;
            Volume = volume;
            IsLoopSound = isLoop;
        }

        public void Play()
        {
            Source.Play();
            IsPlaying = true;
            IsPaused = false;
        }
        public void Pause()
        {
            Source.Pause();
            IsPaused = true;
        }

        public void Stop()
        {
            Source.Stop();
            IsStopped = true;
        }
    }
}
