using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace Code.ConfigData.Audio
{
    public enum SoundStatus
    {
        Playing,
        Stop,
    }
    
    public class BaseAudioFile : ScriptableObject
    {
        public string Id;
        public List<AudioClip> AudioFiles;

        public AudioMixerGroup AudioMixerGroup;
        public SoundStatus SoundStatus;

        public bool IsActivated;


        public bool IsLoopSound;

        public bool IsPlaying { get; private set; }
        public bool IsPaused { get; private set; }
        public bool IsStopped { get; private set; }
        public float Volume { get; set; }
    }
}
