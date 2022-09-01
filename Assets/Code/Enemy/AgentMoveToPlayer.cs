using UnityEngine;
using UnityEngine.AI;

namespace Code.Enemy
{
    public class AgentMoveToPlayer : MonoBehaviour
    {
        private Transform _heroTransform;

        public NavMeshAgent Agent;

        public void Construct(Transform heroTransform)
        {
            _heroTransform = heroTransform;
        }

        private void Update()
        {
            SetDestination();
        }

        private void SetDestination()
        {
            if (_heroTransform != null) Agent.destination = _heroTransform.position;
        }
    }
}