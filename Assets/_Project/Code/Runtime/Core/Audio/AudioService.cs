using System.Collections.Generic;
using System.Linq;
using Code.Runtime.ConfigData.Audio;
using Code.Runtime.ConfigData.Settings;
using Code.Runtime.Core.AssetManagement;
using Code.Runtime.Core.Config;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Audio;
using VContainer;

namespace Code.Runtime.Core.Audio
{
    public class AudioService : MonoBehaviour
    {
        private  IAssetProvider _assetProvider;
        private  ConfigProvider _configProvider;
        
        private readonly Dictionary<string, BaseAudioFile> _sfx = new();
        private readonly Dictionary<string, BaseAudioFile> _bgm = new();
        
        private readonly List<SoundAudioChannel> _soundChannelsPool = new();
        private readonly List<MusicAudioChannel> _musicChannelsPool = new();

        private MusicAudioChannel _mainMusicChannel;
        
        private AudioLibrary _audioLibrary;
        private AudioServiceSettings _audioSettings;

        private AudioMixerGroup _masterMixer;
        private AudioMixerGroup _musicMixer;
        private AudioMixerGroup _sfxMixer;
        
        
        [SerializeField] private Transform _soundChannels;
        [SerializeField] private Transform _musicChannels;

        [Inject]
        public void Construct(IAssetProvider assetProvider, ConfigProvider configProvider)
        {
            _assetProvider = assetProvider;
            _configProvider = configProvider;
        }
        
        public void InitializeAudio()
        {
            _audioLibrary = _configProvider.AudioLibrary();
            _audioSettings = _configProvider.AudioServiceSettings();
            _musicMixer = _audioSettings.MusicMixerGroup;
            _masterMixer = _audioSettings.MasterMixerGroup;
            _sfxMixer = _audioSettings.SfxMixerGroup;
            
            
            for (int i = 0; i < _audioSettings.SoundChannelsPoolSize; i++)
            {
                SoundAudioChannel channel = CreateSoundChannel();
                _soundChannelsPool.Add(channel);
            } 
            
            for (int i = 0; i < _audioSettings.MusicChannelsPoolSize; i++)
            {
                MusicAudioChannel channel = CreateMusicChannel();
                _musicChannelsPool.Add(channel);
            }

            if (_musicChannelsPool.Count > 0)
            {
                _mainMusicChannel = _musicChannelsPool[0];
            }
        }

        public void MuteMusic(bool value)
        {
            _mainMusicChannel.AudioSource.mute = value;
        }

        public void MuteSounds(bool value)
        {
            foreach (var channel in _soundChannelsPool)
            {
                channel.AudioSource.mute = value;
            }
        }

        public void PlayMusic(string musicName, float volume = 1, bool isLoop = false)
        {
            var music = _audioLibrary.Music.FirstOrDefault(x => x.Id == musicName);

            if (music == null)
            {
                Debug.LogError("No music in library with name " + musicName);
            }

            MusicAudioChannel musicChannel = _mainMusicChannel;

            if (musicChannel.IsFree)
            {
                musicChannel.Play(music);
            }
            else if (!musicChannel.IsFree)
            {
                musicChannel.Stop();
                musicChannel.Play(music);
            } 
            else
            {
                Debug.LogError("No music channel");
            }
        }

        public void PlaySound(string soundName, float volume)
        {
            var sound = _audioLibrary.Sounds.FirstOrDefault(x => x.Id == soundName);
            if (sound == null)
            {
                Debug.LogError("No sound in library with name " + soundName);
                return;
            }

            SoundAudioChannel soundChannel = FindFreeSoundChannel();

            if (soundChannel)
            {
                soundChannel.Play(sound);
            }
           
            Assert.IsNull(soundChannel);
        }

        public void PlaySound(SoundAudioFile sound)
        {
            SoundAudioChannel soundChannel = FindFreeSoundChannel();

            if (soundChannel is not null)
            {
                soundChannel.Play(sound);
            }
            
            Assert.IsNull(soundChannel);
        }

        public void PlaySound3D(string soundName, Transform soundTransform, float volume = 1f)
        {
            var sound = _audioLibrary.Sounds.FirstOrDefault(x => x.Id == soundName);
            if (sound == null)
            {
                Debug.LogError("No sound in library with name " + soundName);
                return;
            }
            
          
            SoundAudioChannel soundChannel = FindFreeSoundChannel();

            if (soundChannel is not null)
            {
                soundChannel.SetChannelTransform(soundTransform);
                soundChannel.AudioSource.volume = volume;
                soundChannel.Play(sound);
            }
        }
        
        public void PlaySound3D(SoundAudioFile sound, Transform soundTransform, float volume = 1f)
        {
            SoundAudioChannel soundChannel = FindFreeSoundChannel();

            if (soundChannel is not null)
            {
                soundChannel.SetChannelTransform(soundTransform);
                soundChannel.AudioSource.volume = volume;
                soundChannel.Play(sound);
            }
        }
        
        public void PlaySound3D(string soundName, Vector3 position, float volume = 1f)
        {
            var sound = _audioLibrary.Sounds.FirstOrDefault(x => x.Id == soundName);
            if (sound == null)
            {
                Debug.LogError("No sound in library with name " + soundName);
                return;
            }
            
          
            SoundAudioChannel soundChannel = FindFreeSoundChannel();

            if (soundChannel is not null)
            {
                soundChannel.SetChannelPosition(position);
                soundChannel.AudioSource.volume = volume;
                soundChannel.Play(sound);
            }
        }
        
        public void PlaySound3D(SoundAudioFile sound, Vector3 position, float volume = 1f)
        {
            SoundAudioChannel soundChannel = FindFreeSoundChannel();

            if (soundChannel is not null)
            {
                soundChannel.SetChannelPosition(position);
                soundChannel.AudioSource.volume = volume;
                soundChannel.Play(sound);
            }
        }

        public void PauseMusic()
        {
            if (!_mainMusicChannel.IsFree)
            {
                _mainMusicChannel.AudioSource.Pause();
            }
        }

        public void UnpauseMusic()
        {
            if (!_mainMusicChannel.IsFree)
            {
                _mainMusicChannel.UnPause();
            }
        }
        public void PauseAll()
        {
            foreach (var channel in _soundChannelsPool)
            {
                if (!channel.IsFree)
                {
                    channel.Pause();
                }
            }
            
            if (!_mainMusicChannel.IsFree)
            {
                _mainMusicChannel.Pause();
            }
        }

        public void UnpauseAll()
        {
            foreach (var channel in _soundChannelsPool)
            {
                if (!channel.IsFree)
                {
                    channel.UnPause();
                }
            }
            
            if (!_mainMusicChannel.IsFree)
            {
                _mainMusicChannel.UnPause();
            }
        }

        public void StopMusic()
        {
            if (!_mainMusicChannel.IsFree)
            {
                _mainMusicChannel.Stop();
            }
        }

        public void StopSounds()
        {
            foreach (var channel in _soundChannelsPool)
            {
                if (!channel.IsFree)
                {
                    channel.Stop();
                }
            }
        }

        private SoundAudioChannel FindFreeSoundChannel()
        {
            return _soundChannelsPool.FirstOrDefault(channel => channel.IsFree) ?? CreateSoundChannel();
        }

        private MusicAudioChannel FindFreeMusicChannel()
        {
           return _musicChannelsPool.FirstOrDefault(channel => channel.IsFree) ?? CreateMusicChannel();
        }

        private MusicAudioChannel CreateMusicChannel()
        {
            MusicAudioChannel musicChannel;
            
            if (_audioSettings.MusicChannelPrefab)
            {
                musicChannel = Instantiate(_audioSettings.MusicChannelPrefab, _musicChannels);
            }
            else
            {
                var obj = new GameObject("Music Channel");
                musicChannel = obj.AddComponent<MusicAudioChannel>();
            }
            
            musicChannel.InitializeChannel(_musicChannels, _audioSettings.MusicMixerGroup);
            return musicChannel;
        }

        private SoundAudioChannel CreateSoundChannel()
        {
            SoundAudioChannel soundChannel;
         
            
            if (_audioSettings.SoundChannelPrefab)
            {
                soundChannel = Instantiate(_audioSettings.SoundChannelPrefab, _musicChannels);
            }
            else
            {
                var obj = new GameObject("Sound Channel");
                soundChannel = obj.AddComponent<SoundAudioChannel>();
            }
            
            soundChannel.InitializeChannel(_soundChannels, _audioSettings.SfxMixerGroup);

            return soundChannel;
        }

        public void CleanUp()
        {
           _sfx.Clear();
           _bgm.Clear();
        }
    }
}
