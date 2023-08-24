using UnityEngine;
using UnityEngine.AI;

namespace Code.Entity.Enemy
{
    [RequireComponent(typeof(NavMeshAgent))]
    [RequireComponent(typeof(EnemyAnimator))]
    public class AnimateNavMesh : MonoBehaviour
    {
        private const float MinimalVelocity = 0.1f;

        public NavMeshAgent Agent;
        public EnemyAnimator EnemyAnimator;

        private void Update()
        {
            // if (ShouldMove())
            //     EnemyAnimator.Move(Agent.velocity.magnitude);
            // else
            //     EnemyAnimator.StopMoving();
        }

        private bool ShouldMove()
        {
            return Agent.velocity.magnitude > MinimalVelocity &&
                   Agent.remainingDistance > Agent.radius;
        }
    }
}