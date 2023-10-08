using Code.Data;
using Code.Data.PlayerData;
using UnityEngine;

namespace Code.ConfigData.Ability.PassiveAbilities
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
