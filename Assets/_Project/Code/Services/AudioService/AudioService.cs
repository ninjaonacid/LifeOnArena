using System;
using System.Collections.Generic;
using System.Linq;
using Code.ConfigData.Audio;
using Code.Core.AssetManagement;
using Code.Core.Audio;
using Code.Services.ConfigData;
using UnityEngine;

namespace Code.Services.AudioService
{
    public class AudioService
    {
        private readonly IAssetProvider _assetProvider;
        
        private readonly Dictionary<string, BaseAudioFile> _sfx = new();
        private readonly Dictionary<string, BaseAudioFile> _bgm = new();
        private List<SoundAudioChannel> _soundChannels;
        private List<MusicAudioChannel> _musicChannels;

        private AudioLibrary _audioLibrary;
        
        private readonly IConfigProvider _configProvider;

        private readonly GameAudioPlayer _gameAudioPlayer;

        public AudioService(IAssetProvider assetProvider, IConfigProvider configProvider, GameAudioPlayer gameAudioPlayer)
        {
            _assetProvider = assetProvider;
            _configProvider = configProvider;
            _gameAudioPlayer = gameAudioPlayer;
        }

        public void InitializeAudio ()
        {
            _audioLibrary = _configProvider.GetLibrary();

            // foreach (var sound in library.Sounds)
            // {
            //     _sfx.Add(sound.Id, sound);
            // }
            //
            // foreach (var music in library.Music)
            // {
            //     _bgm.Add(music.Id, music);
            // }
            
            
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
                soundChannel.SetSoundTransform(soundTransform);
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
            foreach (var channel in _soundChannels)
            {
                if (channel.IsFree)
                {
                    return channel;
                }
            }

            return null;
        }

        private MusicAudioChannel CreateMusicChannel()
        {
            return null;
        }

        private SoundAudioChannel CreateSoundChannel()
        {
            return null;
        }


        public void CleanUp()
        {
           _sfx.Clear();
           _bgm.Clear();
        }
    }
}
