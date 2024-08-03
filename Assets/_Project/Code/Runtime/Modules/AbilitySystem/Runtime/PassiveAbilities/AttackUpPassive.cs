using Code.Runtime.Data.PlayerData;
using UnityEngine;

namespace Code.Runtime.Modules.AbilitySystem.PassiveAbilities
{
    public class AttackUpPassive : IPassiveAbility
    {
        private readonly float _attackModifier;

        public AttackUpPassive(float attackModifier)
        {
            _attackModifier = attackModifier;
        }
        public void Apply(GameObject hero, PlayerData heroData)
        {
    
        }
    }
}
