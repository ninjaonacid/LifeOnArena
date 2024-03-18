using UnityEngine;

namespace Code.Runtime.Modules.AbilitySystem
{
    public abstract class Ability
    {
        private float _cooldown;
        private float _activeTime;
        private AbilityState _abilityState;
        
        public abstract void Use(GameObject caster, GameObject target);
    }
}