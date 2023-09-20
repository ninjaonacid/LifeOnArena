using System.Collections.Generic;
using Code.ConfigData.Audio;
using Code.Core.AssetManagement;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Code.Services.AudioService
{
    public class AudioService : IAudioService
    {
        private readonly IAssetProvider _assetProvider;
        private readonly Dictionary<string, AudioClip> _sounds = new();
        public AudioService(IAssetProvider assetProvider)
        {
            _assetProvider = assetProvider;
        }
        public async UniTaskVoid InitAssets(AudioLibrary audioLibrary)
        {
            foreach (var sound in audioLibrary.Sounds)
            {
                var audioClip = await _assetProvider.Load<AudioClip>(sound.SoundRef);
                _sounds.Add(sound.Id, audioClip);
            }
        }

        public void PlaySound(string soundName, AudioSource source)
        {
            if (_sounds.TryGetValue(soundName, out var sound))
            {
                source.PlayOneShot(sound);
            }
        }

        public void CleanUp()
        {
            _sounds.Clear();
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
