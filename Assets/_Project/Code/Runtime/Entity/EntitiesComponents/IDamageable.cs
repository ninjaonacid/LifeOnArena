using Code.Runtime.ConfigData.StatSystem;
using Code.Runtime.Logic.Damage;

namespace Code.Runtime.Entity.EntitiesComponents
{
    public interface IDamageable
    {
        Health Health { get; }
        void TakeDamage(IDamage damage);
    }
}