using System.Collections.Generic;
using Code.Runtime.Entity.Hero;
using Code.Runtime.Entity.StatusEffects;
using UnityEngine;

namespace Code.Runtime.Modules.AbilitySystem.ActiveAbilities
{
    public class DodgeDash : ActiveAbility
    {
        private readonly float _activeTime;
        private readonly float _dashSpeed;
        
        public DodgeDash(IReadOnlyList<GameplayEffect> effects, float cooldown, float activeTime, bool isCastAbility, float dashSpeed) : base(effects, cooldown, activeTime, isCastAbility)
        {
            _activeTime = activeTime;
            _dashSpeed = dashSpeed;
        }

        public override void Use(GameObject caster, GameObject target)
        {
            var heroCollider = caster.GetComponent<HeroHurtBox>();
            var heroMovement = caster.GetComponent<HeroMovement>();
            heroCollider.DisableHitBox(_activeTime);
            heroMovement.DashTask(_activeTime, _dashSpeed);
        }
    }
}
