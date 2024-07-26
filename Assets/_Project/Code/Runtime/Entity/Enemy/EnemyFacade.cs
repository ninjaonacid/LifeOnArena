using Code.Runtime.UI.View.HUD;
using UnityEngine;
using UnityEngine.AI;

namespace Code.Runtime.Entity.Enemy
{
    public class EnemyFacade : MonoBehaviour
    {
        public EnemyDeath EnemyDeath;
        public EnemyWeapon EnemyWeapon;
        public EnemyTarget EnemyTarget;
        public EnemyStats EnemyStats;
        public NavMeshMoveToPlayer NavMeshMove;
        public NavMeshAgent NavMeshAgent;
        public LootSpawner EnemyLootSpawner;
        public ExpDrop ExpDrop;
        public EntityUI EntityUI;
        public EnemyHealth EnemyHealth;
        public EnemyStaggerComponent EnemyStaggerComponent;

        private void OnValidate()
        {
            EnemyDeath ??= GetComponent<EnemyDeath>();
            EnemyWeapon ??= GetComponent<EnemyWeapon>();
            EnemyTarget ??= GetComponent<EnemyTarget>();
            EnemyStats ??= GetComponent<EnemyStats>();
            NavMeshMove ??= GetComponent<NavMeshMoveToPlayer>();
            NavMeshAgent ??= GetComponent<NavMeshAgent>();
            EnemyLootSpawner ??= GetComponentInChildren<LootSpawner>();
            ExpDrop = GetComponent<ExpDrop>();
            EntityUI = GetComponent<EntityUI>();
            EnemyHealth = GetComponent<EnemyHealth>();
            EnemyStaggerComponent = GetComponent<EnemyStaggerComponent>();
        }
    }
}