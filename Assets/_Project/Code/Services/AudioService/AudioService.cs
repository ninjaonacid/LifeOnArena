using System;
using System.Collections.Generic;
using Code.ConfigData.Audio;
using Code.Core.AssetManagement;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Code.Services.AudioService
{
    public class AudioService : IAudioService
    {
        private readonly IAssetProvider _assetProvider;
        
        private readonly Dictionary<string, Sound> _sfx = new();
        private readonly Dictionary<string, Sound> _bgm = new();

        private Transform _gameAudioSource;
        public AudioService(IAssetProvider assetProvider)
        {
            _assetProvider = assetProvider;
        }
        public async UniTaskVoid InitializeAudio(AudioLibrary audioLibrary)
        {
            if (!_gameAudioSource)
            {
                _assetProvider.InstantiateSync(AssetAddress.GameAudioSource);
            }
            
            foreach (var sound in audioLibrary.Sounds)
            {
                await _assetProvider.Load<AudioClip>(sound.SoundRef);
                _sfx.Add(sound.Id, sound);
            }

            foreach (var music in audioLibrary.Music)
            {
                await _assetProvider.Load<AudioClip>(music.SoundRef);
                _bgm.Add(music.Id, music);
            }
        }

        public void PlayBackgroundMusic(string musicName, float volume)
        {
            if (_bgm.TryGetValue(musicName, out var music))
            {
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
            var sound = await _assetProvider.Load<AudioClip>(AssetAddress.HeroSwordAttackSound);
            //GameObject soundGameObject = new GameObject();
            //AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();

            audioSource.PlayOneShot(sound);
        }
    }
}
