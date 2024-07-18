using UnityEngine;
using UnityEngine.AI;

namespace Code.Runtime.Entity.Enemy
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class AnimateNavMesh : MonoBehaviour
    {
        private const float MinimalVelocity = 0.1f;
        [SerializeField] private NavMeshAgent _agent;
        

        private bool ShouldMove()
        {
            return _agent.velocity.magnitude > MinimalVelocity &&
                   _agent.remainingDistance > _agent.radius;
        }
    }
}