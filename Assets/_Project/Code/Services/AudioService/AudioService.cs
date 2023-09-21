using System;
using System.Collections.Generic;
using System.Threading;
using Code.ConfigData.Audio;
using Code.Core.AssetManagement;
using Code.Core.Audio;
using Code.Services.ConfigData;
using Cysharp.Threading.Tasks;
using UnityEngine;
using VContainer.Unity;

namespace Code.Services.AudioService
{
    public class AudioService
    {
        private readonly IAssetProvider _assetProvider;
        
        private readonly Dictionary<string, Sound> _sfx = new();
        private readonly Dictionary<string, Sound> _bgm = new();
        private readonly IConfigProvider _configProvider;

        private GameAudioPlayer _gameAudioPlayer;

        public AudioService(IAssetProvider assetProvider, IConfigProvider configProvider, GameAudioPlayer gameAudioPlayer)
        {
            _assetProvider = assetProvider;
            _configProvider = configProvider;
            _gameAudioPlayer = gameAudioPlayer;
        }

        public void InitializeAudio ()
        {
            var library = _configProvider.GetLibrary();

            foreach (var sound in library.Sounds)
            {
                _sfx.Add(sound.Id, sound);
            }

            foreach (var music in library.Music)
            {
                _bgm.Add(music.Id, music);
            }
        }

        public void PlayBackgroundMusic(string musicName, float volume = 1, bool isLoop = false)
        {
            if (_bgm.TryGetValue(musicName, out var music))
            {
                music.Setup(_gameAudioPlayer.GetAudioSource(), volume, isLoop);
               
                music.Play();
            }
            else
            {
                throw new Exception($"No music sound in library with name : {musicName}");
            }
        }

        public void PlaySound(string soundName, float volume)
        {
            if (_sfx.TryGetValue(soundName, out var sound))
            {

                sound.Source = _gameAudioPlayer.GetAudioSource();
                sound.Volume = volume;
                sound.Play();
                //PrepareSound(sound);
                //source.PlayOneShot(sound);
            }
        }

        public void PlaySound3D(string soundName, Transform soundTransform, float volume)
        {
            if (!_sfx.TryGetValue(soundName, out var sound)) return;
            
            if(!soundTransform.gameObject.TryGetComponent(out AudioSource soundSource))
            {
                soundSource = soundTransform.gameObject.AddComponent<AudioSource>();
            }
            
            sound.Setup(soundSource, volume, false);
            sound.Play();
            //PrepareSound(sound);
            //source.PlayOneShot(sound);
        }

        private void PreloadSound(string name)
        {
            if (_sfx.TryGetValue(name, out var sound))
            {
                
            }
        }
        private void PrepareSound(Sound sound, Transform soundTransform, float volume)
        {
            //sound.SourceTransform = soundTransform;
            sound.Source = soundTransform.gameObject.AddComponent<AudioSource>();
            sound.Volume = volume;
        }


        public void CleanUp()
        {
           _sfx.Clear();
           _bgm.Clear();
        }
    }
}
