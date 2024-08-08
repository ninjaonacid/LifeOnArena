using Code.Runtime.Modules.StatSystem;
using Code.Runtime.Modules.StatSystem.Runtime;

namespace Code.Runtime.Entity.Enemy
{
    public class EnemyStats : StatController
    {
        protected override void Awake()
        {
            
        }

        public void SetStats(StatDatabase statDatabase)
        {
            if (!IsInitialized)
            {
                _statDatabase = statDatabase;
                Initialize();
            }
        }
    }
}