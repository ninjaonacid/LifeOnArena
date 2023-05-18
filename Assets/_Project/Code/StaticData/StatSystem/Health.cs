using System.Collections.Generic;
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
        }

        
    }
}
