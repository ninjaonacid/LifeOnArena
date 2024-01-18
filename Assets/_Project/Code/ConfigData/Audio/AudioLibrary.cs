using System.Collections.Generic;
using UnityEngine;

namespace Code.ConfigData.Audio
{
    [CreateAssetMenu(menuName = "Config/Audio/AudioLibrary", fileName = "AudioLibrary")]
    public class AudioLibrary : ScriptableObject
    {
        public List<SoundAudioFile> Sounds;
        public List<MusicAudioFile> Music;
    }
}
