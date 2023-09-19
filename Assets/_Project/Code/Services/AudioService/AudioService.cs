using System.Collections.Generic;
using Code.ConfigData.Audio;
using Code.Infrastructure.AssetManagement;
using UnityEngine;

namespace Code.Services.AudioService
{
    public class AudioService : IAudioService
    {
        private readonly IAssetProvider _assetProvider;
        private Dictionary<SoundId, Sound> _sounds = new();
        public AudioService(IAssetProvider assetProvider)
        {
            _assetProvider = assetProvider;
        }
        public void InitAssets(AudioLibrary audioLibrary)
        {
            foreach (var sound in audioLibrary.Sounds)
            {
                _assetProvider.Load<AudioClip>(sound.SoundRef);
                _sounds.Add();
            }
           // _assetProvider.Load<AudioClip>(AssetAddress.HeroSwordAttackSound);
        }

        public void PlaySound()
        {
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
