using Code.ConfigData.StatSystem;
using Code.Logic.Damage;

namespace Code.Entity.EntitiesComponents
{
    public interface IDamageable
    {
        Health Health { get; }
        void TakeDamage(IDamage damage);
    }
}