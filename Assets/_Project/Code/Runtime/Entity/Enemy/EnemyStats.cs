using Code.Runtime.Modules.StatSystem;
using UnityEngine;

namespace Code.Runtime.Entity.Enemy
{
    public class EnemyStats : StatController
    {
        
        protected override void Awake()
        {
            
        }

        public void SetStats(StatDatabase statDatabase)
        {
            _statDatabase = statDatabase;
            Initialize();
        }
        
    }
}