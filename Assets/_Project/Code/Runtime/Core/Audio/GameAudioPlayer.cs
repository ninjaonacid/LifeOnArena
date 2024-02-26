using UnityEngine;

namespace Code.Runtime.Core.Audio
{
    [RequireComponent(typeof(AudioSource))]
    public class GameAudioPlayer : MonoBehaviour
    {
        [SerializeField] private AudioSource _gameAudioSource;
        
        public AudioSource GetAudioSource() => _gameAudioSource;
    }
}
