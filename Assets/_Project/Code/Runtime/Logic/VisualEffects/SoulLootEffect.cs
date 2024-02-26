using System;
using Code.Runtime.Core.Factory;
using UnityEngine;
using VContainer;
using Vector3 = UnityEngine.Vector3;

namespace Code.Runtime.Logic.VisualEffects
{
    public class SoulLootEffect : VisualEffect
    {
        private readonly ParticleSystem.Particle[] _particles = new ParticleSystem.Particle[100];

        private Transform _targetTransform;

        private readonly float _speed = 10f;
        
        [Inject]
        public void Construct(IHeroFactory heroFactory)
        {
            _targetTransform = heroFactory.HeroGameObject.transform;
        }
        

        private void LateUpdate()
        {
            var particlesCount = _particleSystem.GetParticles(_particles);
            
            
            for (int i = 0; i <= particlesCount; i++)
            {
                var particle = _particles[i];
                
                particle.position =
                    Vector3.MoveTowards(particle.position, _targetTransform.position, _speed * Time.deltaTime);
            
                if (Vector3.Distance(particle.position, _targetTransform.position) <= 0.5f)
                {
                    particle.remainingLifetime = -1;
                }

                _particles[i] = particle;
            }
            
            _particleSystem.SetParticles(_particles, particlesCount);

           
            
        }
    }
}
