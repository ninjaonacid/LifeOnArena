using Code.Infrastructure.AssetManagment;
using UnityEngine;

namespace Code.Services.AudioService
{
    public class AudioService : IAudioService
    {
        private readonly IAssetsProvider _assetsProvider;

        public AudioService(IAssetsProvider assetsProvider)
        {
            _assetsProvider = assetsProvider;
            
        }
        public void InitAssets()
        {
            _assetsProvider.Load<AudioClip>(AssetAddress.HeroSwordAttackSound);
        }

        public void PlaySound()
        {
        }

        public async void PlayHeroAttackSound(AudioSource audioSource)
        {
            var sound = await _assetsProvider.Load<AudioClip>(AssetAddress.HeroSwordAttackSound);
            //GameObject soundGameObject = new GameObject();
            //AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();

            audioSource.PlayOneShot(sound);
        }
    }
}
