using System.Collections.Generic;

namespace Code.Logic.Damage
{
    public interface IDamageSource
    {
        public IReadOnlyList<DamageType> DamageTypes { get; }
    }
}
