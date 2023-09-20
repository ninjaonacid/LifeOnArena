using System;
using System.Collections.Generic;
using Code.ConfigData.Audio;
using Code.Core.AssetManagement;
using Code.Core.Audio;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Code.Services.AudioService
{
    public class AudioService 
    {
        private readonly IAssetProvider _assetProvider;
        
        private readonly Dictionary<string, Sound> _sfx = new();
        private readonly Dictionary<string, Sound> _bgm = new();

        private GameAudioPlayer _gameAudioPlayer;
        public AudioService(IAssetProvider assetProvider, GameAudioPlayer gameAudioPlayer)
        {
            _assetProvider = assetProvider;
            _gameAudioPlayer = gameAudioPlayer;
        }
        public async UniTask InitializeAudio(AudioLibrary audioLibrary)
        {
            if (!_gameAudioPlayer.TryGetComponent<AudioSource>(out var source))
            {
                source = _gameAudioPlayer.gameObject.AddComponent<AudioSource>();
            }


            List<UniTask<AudioClip>> tasks = new List<UniTask<AudioClip>>();
            
            foreach (var sound in audioLibrary.Sounds)
            {
               var task = _assetProvider.Load<AudioClip>(sound.SoundRef);
               tasks.Add(task);
               _sfx.Add(sound.Id, sound);
            }

            foreach (var music in audioLibrary.Music)
            {
                
                var task =  _assetProvider.Load<AudioClip>(music.SoundRef);
                tasks.Add(task);
                _bgm.Add(music.Id, music);
            }

            await UniTask.WhenAll(tasks);
        }

        public void PlayBackgroundMusic(string musicName, float volume)
        {
            if (_bgm.TryGetValue(musicName, out var music))
            {
                if(_gameAudioPlayer.TryGetComponent<AudioSource>(out var source))
                {
                    music.Source = source;
                    music.Volume = volume;
                }
                music.Play();
            }
        
            else
            {
                throw new Exception($"No music sound in library with name : {musicName}");
            }
        }
        public void PlaySound(string soundName, float volume)
        {
            
        }
        public void PlaySound3D(string soundName, Transform soundTransform)
        {
            
            if (_sfx.TryGetValue(soundName, out var sound))
            {
                //PrepareSound(sound);
                //source.PlayOneShot(sound);
            }
        }

        private void PrepareSound(Sound sound, Transform soundTransform, float volume)
        {
            sound.SourceTransform = soundTransform;
            sound.Source = soundTransform.gameObject.AddComponent<AudioSource>();
            sound.Volume = volume;

        }
        public void CleanUp()
        {
           _sfx.Clear();
           _bgm.Clear();
        }
        
        public void PlaySound()
        {
            throw new System.NotImplementedException();
        }

        public async void PlayHeroAttackSound(AudioSource audioSource)
        {
            //var sound = await _assetProvider.Load<AudioClip>(AssetAddress.HeroSwordAttackSound);
            //GameObject soundGameObject = new GameObject();
            //AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();

            //audioSource.PlayOneShot(sound);
        }
    }
}
