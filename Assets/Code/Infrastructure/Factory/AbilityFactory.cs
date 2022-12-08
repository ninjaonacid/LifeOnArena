using System;
using System.Collections.Generic;
using Code.Data;
using Code.Hero.Abilities;
using Code.Hero.HeroStates;
using Code.Services;
using Code.Services.PersistentProgress;
using Code.StaticData.Ability;
using Code.UI.HUD.Skills;

namespace Code.Infrastructure.Factory
{
    public class AbilityFactory : IAbilityFactory
    {
        private readonly IStaticDataService _staticData;
        private IProgressService _progress;

        private readonly Dictionary<AbilityId, Type> _abilityStates;

        public AbilityFactory(IStaticDataService staticData, IProgressService progress)
        {
            _staticData = staticData;
            _progress = progress;

            _abilityStates = new Dictionary<AbilityId, Type>()
            {
                [AbilityId.SpinAttack] = typeof(SpinAttackState),
                [AbilityId.FastSlash] = typeof(FastSlashSkill)
            };
        }
        public Ability CreateAbility(AbilityId abilityId)
        {
            var abilityData = _staticData.ForAbility(abilityId);
            if (abilityData == null) return null;
            var ability = new Ability(
                abilityData.Damage, 
                abilityData.Cooldown, 
                abilityData.AbilityId,
                abilityData.SkillIcon,
                GetAbilityState(abilityData.AbilityId));
            return ability;
        }

        private Type GetAbilityState(AbilityId abilityId) => _abilityStates[abilityId];
    }
}
