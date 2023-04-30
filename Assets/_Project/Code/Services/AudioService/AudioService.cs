using Code.Infrastructure.AssetManagment;
using UnityEngine;

namespace Code.Services.AudioService
{
    public class AudioService : IAudioService
    {
        private readonly IAssetProvider _assetProvider;

        public AudioService(IAssetProvider assetProvider)
        {
            _assetProvider = assetProvider;
            
        }
        public void InitAssets()
        {
            _assetProvider.Load<AudioClip>(AssetAddress.HeroSwordAttackSound);
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
