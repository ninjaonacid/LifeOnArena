using UnityEngine;
using UnityEngine.AI;

namespace Code.Runtime.Entity.Enemy
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class AgentMoveToPlayer : MonoBehaviour
    {
        [SerializeField] private NavMeshAgent Agent;
        
        private Transform _heroTransform;
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
            if(gameObject.activeSelf)
                Agent.isStopped = !context;
        }
    }
}