using Code.ConfigData.Identifiers;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Code.ConfigData
{
    [CreateAssetMenu(fileName = "ParticleEffect", menuName = "StaticData/Particle")]
    public class ParticleData : ScriptableObject
    {
        public Identifier Identifier;
        public AssetReference ParticleReference;
    }
}
