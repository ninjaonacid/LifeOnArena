using System;
using UnityEngine;

namespace Code.Core.Audio
{
    [RequireComponent(typeof(AudioSource))]
    public class GameAudioPlayer : MonoBehaviour
    {
        [SerializeField] private AudioSource _gameAudioSource;
        
        public AudioSource GetAudioSource() => _gameAudioSource;
    }
}
