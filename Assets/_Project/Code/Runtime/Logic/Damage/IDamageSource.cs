using System.Collections.Generic;

namespace Code.Runtime.Logic.Damage
{
    public interface IDamageSource
    {
        public IReadOnlyList<DamageType> DamageTypes { get; }
    }
}
