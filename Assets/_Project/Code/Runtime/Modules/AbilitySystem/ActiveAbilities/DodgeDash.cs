using Code.Runtime.Entity.Hero;
using UnityEngine;

namespace Code.Runtime.Modules.AbilitySystem.ActiveAbilities
{
    public class DodgeDash : IAbility
    {
        private readonly float _activeTime;
        private readonly float _dashSpeed;
        public DodgeDash(float activeTime, float dashSpeed)
        {
            _activeTime = activeTime;
            _dashSpeed = dashSpeed;
        }
        public void Use(GameObject caster, GameObject target)
        {
            var heroCollider = caster.GetComponent<HeroHurtBox>();
            var heroMovement = caster.GetComponent<HeroMovement>();
            heroCollider.DisableHitBox(_activeTime);
            heroMovement.DashTask(_activeTime, _dashSpeed);
        }
    }
}