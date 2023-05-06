using Code.StaticData.Identifiers;
using UnityEngine;

namespace Code.StaticData
{
    [CreateAssetMenu(fileName = "ParticleEffect", menuName = "StaticData/Particle")]
    public class ParticlesStaticData : ScriptableObject
    {
        public ParticleId ParticleId;
        public GameObject ParticlePrefab;
    }
}
