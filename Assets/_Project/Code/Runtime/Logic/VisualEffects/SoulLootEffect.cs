using Code.Runtime.Core.Factory;
using Code.Runtime.Entity;
using UnityEngine;
using VContainer;
using Vector3 = UnityEngine.Vector3;

namespace Code.Runtime.Logic.VisualEffects
{
    public class SoulLootEffect : VisualEffect
    {
        private readonly ParticleSystem.Particle[] _particles = new ParticleSystem.Particle[100];
        private EntityHurtBox _hurtBox;
        private float _startDistance;
        private float _speed = 10f;
        
        [Inject]
        public void Construct(IHeroFactory heroFactory)
        {
            _hurtBox = heroFactory.HeroGameObject.GetComponent<EntityHurtBox>();
        }

        private void Start()
        {
            _startDistance = Vector3.Distance(transform.position, _hurtBox.GetCenterTransform());
        }

        private void LateUpdate()
        {
            var particlesCount = _particleSystem.GetParticles(_particles);
     
            for (int i = 0; i <= particlesCount; i++)
            {
                var particle = _particles[i];

                var targetPosition = _hurtBox.GetCenterTransform();
                particle.position =
                    Vector3.MoveTowards(particle.position, targetPosition, _speed * Time.deltaTime);
                
                var currentDistance = Vector3.Distance(particle.position, targetPosition);
                
                if (currentDistance > _startDistance)
                {
                    _speed += 0.1f;
                }
                if (currentDistance <= 0.5f)
                {
                    particle.remainingLifetime = -1;
                }

                _particles[i] = particle;
            }
            
            _particleSystem.SetParticles(_particles, particlesCount);
            
        }
    }
}
