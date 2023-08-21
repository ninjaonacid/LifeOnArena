using UnityEngine;
using UnityEngine.AI;

namespace Code.Entity.Enemy
{
    public class AgentMoveToPlayer : MonoBehaviour
    {
        private Transform _heroTransform;

        public NavMeshAgent Agent;

        public void Construct(Transform heroTransform)
        {
            _heroTransform = heroTransform;
        }
        public void SetDestination()
        {
            if (_heroTransform != null) Agent.destination = _heroTransform.position;
        }

        public void ShouldMove(bool context)
        {
            Agent.isStopped = !context;
        }
    }
}