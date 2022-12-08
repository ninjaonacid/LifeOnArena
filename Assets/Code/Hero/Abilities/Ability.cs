using System;
using Code.Hero.HeroStates;
using Code.StaticData.Ability;
using UnityEngine;

namespace Code.Hero.Abilities
{
    public class Ability
    {
        public float Damage;

        public float Cooldown;

        public Type StateMachineState;

        public Sprite AbilityIcon;

        public AbilityId AbilityId;

        public Ability(float damage, float cooldown, AbilityId abilityId, Sprite icon, Type stateMachinestate)
        {
            Damage = damage;
            Cooldown = cooldown;
            AbilityId = abilityId;
            StateMachineState = stateMachinestate;
            AbilityIcon = icon;

        }
    }
}
