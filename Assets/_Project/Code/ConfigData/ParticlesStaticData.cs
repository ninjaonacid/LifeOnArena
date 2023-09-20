using Code.ConfigData.Identifiers;
using UnityEngine;

namespace Code.ConfigData
{
    [CreateAssetMenu(fileName = "ParticleEffect", menuName = "StaticData/Particle")]
    public class ParticlesStaticData : ScriptableObject
    {
        public ParticleId ParticleId;
        public GameObject ParticlePrefab;
    }
}
