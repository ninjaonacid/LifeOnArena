using Code.ConfigData.Identifiers;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Serialization;

namespace Code.ConfigData
{
    [CreateAssetMenu(fileName = "ParticleEffect", menuName = "Config/Particle")]
    public class VfxData : ScriptableObject
    {
        public ParticleIdentifier Identifier;
        public AssetReference PrefabReference;
    }
}
