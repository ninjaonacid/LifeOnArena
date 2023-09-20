using System;
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
        public Transform SourceTransform;
        public bool UnloadSound;
        public bool IsPlaying;
        public bool Paused;
        public bool Stopped;
        public float Volume;


        public void Play()
        {
            Source.Play();
        }
    }
}
