using Code.ConfigData.Identifiers;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Serialization;

namespace Code.ConfigData
{
    [CreateAssetMenu(fileName = "ParticleEffect", menuName = "StaticData/Particle")]
    public class ViewObjectData : ScriptableObject
    {
        public ViewIdentifier Identifier;
        public AssetReference ViewReference;
    }
}
