using System.Collections.Generic;
using System.Linq;
using Code.ConfigData.Audio;
using Code.ConfigData.Settings;
using Code.Core.AssetManagement;
using Code.Services.ConfigData;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Audio;
using VContainer;

namespace Code.Core.Audio
{
    public class AudioService : MonoBehaviour
    {
        private  IAssetProvider _assetProvider;
        private  IConfigProvider _configProvider;
        
        private readonly Dictionary<string, BaseAudioFile> _sfx = new();
        private readonly Dictionary<string, BaseAudioFile> _bgm = new();
        
        private List<SoundAudioChannel> _soundChannelsPool = new();
        private List<MusicAudioChannel> _musicChannelsPool = new();

        private MusicAudioChannel _mainMusicChannel;
        
        private AudioLibrary _audioLibrary;
        private AudioServiceSettings _audioSettings;

        private AudioMixerGroup _masterMixer;
        private AudioMixerGroup _musicMixer;
        private AudioMixerGroup _sfxMixer;
        
        
        [SerializeField] private Transform _soundChannels;
        [SerializeField] private Transform _musicChannels;

        [Inject]
        public void Construct(IAssetProvider assetProvider, IConfigProvider configProvider)
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

        public void MuteMusic()
        {
            _musicMixer.audioMixer.SetFloat("Volume", -80f);
        }

        public void PlayBackgroundMusic(string musicName, float volume = 1, bool isLoop = false)
        {
            var music = _audioLibrary.Music.FirstOrDefault(x => x.Id == musicName);

            if (music == null)
            {
                Debug.LogError("No music in library with name " + musicName);
            }

            MusicAudioChannel musicChannel = FindFreeMusicChannel();

            if (musicChannel)
            {
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

            if (soundChannel)
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

            if (soundChannel)
            {
                soundChannel.SetChannelTransform(soundTransform);
                soundChannel.AudioSource.volume = volume;
                soundChannel.Play(sound);
            }
        }

        private void PreloadSound(string name)
        {
            if (_sfx.TryGetValue(name, out var sound))
            {
                
            }
        }
        private void PrepareSound(BaseAudioFile baseAudioFile, Transform soundTransform, float volume)
        {
            //sound.SourceTransform = soundTransform;
           // sound.Source = soundTransform.gameObject.AddComponent<AudioSource>();
            baseAudioFile.Volume = volume;
        }

        private SoundAudioChannel FindFreeSoundChannel()
        {
            foreach (var channel in _soundChannelsPool)
            {
                if (channel.IsFree)
                {
                    return channel;
                }
            }

            return CreateSoundChannel();
        }

        private MusicAudioChannel FindFreeMusicChannel()
        {
            foreach (var channel in _musicChannelsPool)
            {
                if (channel.IsFree)
                {
                    return channel;
                }
            }

            return CreateMusicChannel();
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

        private void StopMusic()
        {
            if (!_mainMusicChannel.IsFree)
            {
                _mainMusicChannel.Stop();
            }
        }
        
        

        private void StopAllSounds()
        {
            foreach (var channel in _soundChannelsPool)
            {
                if (!channel.IsFree)
                {
                    channel.Stop();
                }
            }
        }
        

        public void CleanUp()
        {
           _sfx.Clear();
           _bgm.Clear();
        }
    }
}
