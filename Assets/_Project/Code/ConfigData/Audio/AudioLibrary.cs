using System.Collections.Generic;
using UnityEngine;

namespace Code.ConfigData.Audio
{
    [CreateAssetMenu(menuName = "ConfigData", fileName = "AudioLibrary")]
    public class AudioLibrary : ScriptableObject
    {
        public List<Sound> Sounds;
    }
}
