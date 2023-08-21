using Code.Entity.Hero;
using UnityEngine;

namespace Code.StaticData.Ability.ActiveAbilities
{
    public class DodgeRoll : IAbility
    {
        private readonly float _activeTime;
        public DodgeRoll(float activeTime)
        {
            _activeTime = activeTime;
        }
        public void Use(GameObject caster, GameObject target)
        {
            var heroCollider = caster.GetComponent<HeroHitBox>();
            heroCollider.DisableHitBox(_activeTime);
        }

    }
}
