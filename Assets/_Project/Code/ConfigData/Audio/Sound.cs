using System;
using UnityEngine;

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
        public AudioClip Clip;
        public AudioSource Source;
        public Transform SourceTransform { get; private set; }
        public bool IsPlaying { get; private set; }
        public bool IsPaused { get; private set; }
        public bool IsStopped { get; private set; }
        public float Volume { get; set; }
        
        
        public bool IsLoopSound;

        public void Setup(AudioSource source, float volume, bool isLoop)
        {
            Source = source;
            Volume = volume;
            IsLoopSound = isLoop;
            
            
            Source.clip = Clip;
            Source.volume = volume;
            Source.loop = isLoop;
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
