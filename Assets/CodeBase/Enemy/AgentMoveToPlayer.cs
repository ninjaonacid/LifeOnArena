using CodeBase.Infrastructure.Factory;
using CodeBase.Infrastructure.Services;
using System;
using UnityEngine;
using UnityEngine.AI;

namespace CodeBase
{
    public class AgentMoveToPlayer : MonoBehaviour
    {
        private float MinDistance = 1;

        public NavMeshAgent Agent;
        private Transform _heroTransform;
        private IGameFactory _gameFactory;


        private void Start()
        {
            _gameFactory = AllServices.Container.Single<IGameFactory>();
            if (_gameFactory.HeroGameObject != null)
            {
                InitializeHeroTransform();
            }
            else
            {
                _gameFactory.HeroCreated += HeroCreated;
            }
        }
        void Update()
        {


            if (_heroTransform != null && HeroNotReached())
            {
                Agent.destination = _heroTransform.position;
                
            }
            
        }

        private void HeroCreated()
        {
            InitializeHeroTransform();
        }

        private void InitializeHeroTransform()
        {
            _heroTransform = _gameFactory.HeroGameObject.transform;
        }


        private bool  HeroNotReached() =>
          Vector3.Distance(Agent.transform.position, _heroTransform.position) >= MinDistance;
        
        
    }
}
