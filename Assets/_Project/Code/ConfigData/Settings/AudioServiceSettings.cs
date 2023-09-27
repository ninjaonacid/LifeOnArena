using Code.Core.Audio;
using UnityEngine;
using UnityEngine.Audio;

namespace Code.ConfigData.Settings
{
    [CreateAssetMenu(menuName = "Settings/Audio", fileName = "AudioSettings")]
    public class AudioServiceSettings : ScriptableObject
    {
        [SerializeField] private int _soundChannelsPoolSize;
        public int SoundChannelsPoolSize => _soundChannelsPoolSize;

        [SerializeField] private int _musicChannelsPoolSize;
        public int MusicChannelsPoolSize => _musicChannelsPoolSize;

        public SoundAudioChannel SoundChannelPrefab;
        public MusicAudioChannel MusicChannelPrefab;

        public AudioMixer Mixer;
        public AudioMixerGroup DefaultMixerGroup;
        public AudioMixerGroup MusicMixerGroup;

    }
}
