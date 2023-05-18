using System.Collections.Generic;
using System.Linq;
using Code.Logic.Damage;
using UnityEngine;

namespace Code.StaticData.StatSystem
{
    public class Health : Attribute
    {
        
        public Health(StatDefinition statDefinition, StatController statController) : base(statDefinition, statController)
        {
        }


        public override void ApplyModifier(StatModifier modifier)
        {
            var damageSource = modifier.Source as IDamageSource;

            if (damageSource == null) return;
            
            if (damageSource.DamageTypes.Contains(DamageType.Physical))
            {
                modifier.Magnitude += _statController.Stats["Defense"].Value;
            }
        }

        
    }
}
