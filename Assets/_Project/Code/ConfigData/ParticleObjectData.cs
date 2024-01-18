using Code.ConfigData.Identifiers;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Code.ConfigData
{
    [CreateAssetMenu(fileName = "ParticleEffect", menuName = "Config/Particle")]
    public class ParticleObjectData : ScriptableObject
    {
        public ParticleIdentifier Identifier;
        public AssetReference ViewReference;
    }
}
