using System;
using System.Collections.Generic;
using UnityEngine;

namespace Code.ConfigData.Audio
{
    public enum SoundStatus
    {
        Playing,
        Stop,
    }
    [Serializable]
    public class BaseAudioFile
    {
        public string Id;
        public List<AudioClip> AudioFiles;

        public SoundStatus SoundStatus;
    
        public AudioSource Source { get; private set; }

        public Transform SourceTransform { get; private set; }
        public bool IsPlaying { get; private set; }
        public bool IsPaused { get; private set; }
        public bool IsStopped { get; private set; }

        public bool IsActivated;
        public float Volume { get; set; }
        
        
        public bool IsLoopSound;
        

        public void Play()
        {
            Source.Play();
            SoundStatus = SoundStatus.Playing;
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
            SoundStatus = SoundStatus.Stop;
            IsStopped = true;
        }
    }
}
