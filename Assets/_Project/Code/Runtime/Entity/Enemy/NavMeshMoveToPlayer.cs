using UnityEngine;
using UnityEngine.AI;

namespace Code.Runtime.Entity.Enemy
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class NavMeshMoveToPlayer : MonoBehaviour
    {
        [SerializeField] private NavMeshAgent _navMeshAgent;
        
        private Transform _heroTransform;
        public void Construct(Transform heroTransform)
        {
            _heroTransform = heroTransform;
        }
        public void SetDestination()
        {
            if (_heroTransform != null) _navMeshAgent.destination = _heroTransform.position;
        }

        public void ShouldMove(bool context)
        {
            if(gameObject.activeSelf)
                _navMeshAgent.isStopped = !context;
        }

        public void Warp(Vector3 position)
        {
            _navMeshAgent.Warp(position);
        }
    }
}