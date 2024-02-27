using Code.Runtime.ConfigData.Identifiers;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Code.Runtime.ConfigData
{
    [CreateAssetMenu(fileName = "ParticleEffect", menuName = "Config/Particle")]
    public class VisualEffectData : ScriptableObject
    {
        public VisualEffectIdentifier Identifier;
        public AssetReference PrefabReference;
    }
}
