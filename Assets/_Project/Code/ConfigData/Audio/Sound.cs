using System;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Code.ConfigData.Audio
{
    public enum SoundId
    {
        SwordSlash = 1,
    }
    [Serializable]
    public class Sound
    {
        public AssetReference SoundRef;
        public string Id;
    }
}
