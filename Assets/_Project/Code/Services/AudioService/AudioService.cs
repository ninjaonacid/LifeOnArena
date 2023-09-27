using System;
using System.Collections.Generic;
using System.Linq;
using Code.ConfigData.Audio;
using Code.ConfigData.Settings;
using Code.Core.AssetManagement;
using Code.Core.Audio;
using Code.Services.ConfigData;
using UnityEngine;
using UnityEngine.Audio;
using VContainer;

namespace Code.Services.AudioService
{
    public class AudioService : MonoBehaviour
    {
        private  IAssetProvider _assetProvider;
        private  IConfigProvider _configProvider;
        
        private readonly Dictionary<string, BaseAudioFile> _sfx = new();
        private readonly Dictionary<string, BaseAudioFile> _bgm = new();
        private List<SoundAudioChannel> _soundChannelsPool;
        private List<MusicAudioChannel> _musicChannelsPool;

        private MusicAudioChannel _mainMusicChannel;
        
        private AudioLibrary _audioLibrary;
        private AudioServiceSettings _audioSettings;
        private Transform _soundChannels;
        private Transform _musicChannels;
        
        
        
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

        public void PlayBackgroundMusic(string musicName, float volume = 1, bool isLoop = false)
        {
            if (_bgm.TryGetValue(musicName, out var music))
            {
                
            }
            else
            {
                throw new Exception($"No music sound in library with name : {musicName}");
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
            else
            {
                Debug.LogError("Noasd");
            }
            
        }

        public void PlaySound3D(string soundName, Transform soundTransform, float volume)
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
            var obj = new GameObject();
            obj.transform.SetParent(_musicChannels);
            var channel = gameObject.AddComponent<MusicAudioChannel>();
            channel.InitializeChannel(transform, _audioSettings.MusicMixerGroup);
            return channel;
        }

        private SoundAudioChannel CreateSoundChannel()
        {
            var obj = new GameObject();
            obj.transform.SetParent(_soundChannels);
            var channel = gameObject.AddComponent<SoundAudioChannel>();
            //channel.InitializeChannel(transform, );
            return channel;
        }


        public void CleanUp()
        {
           _sfx.Clear();
           _bgm.Clear();
        }
    }
}
