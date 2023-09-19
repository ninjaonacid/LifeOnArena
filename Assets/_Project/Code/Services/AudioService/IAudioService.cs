using Code.ConfigData.Audio;
using UnityEngine;

namespace Code.Services.AudioService
{
    public interface IAudioService : IService
    {
        void PlaySound();
        void PlayHeroAttackSound(AudioSource audioSource);
        void InitAssets(AudioLibrary audioLibrary);
    }
}
