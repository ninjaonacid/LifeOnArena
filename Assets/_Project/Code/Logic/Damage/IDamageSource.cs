using System.Collections.Generic;
using UnityEngine;

namespace Code.Logic.Damage
{
    public interface IDamageSource
    {
        public IReadOnlyList<DamageType> DamageTypes { get; }
    }
}
