using System.Collections.Generic;
using Code.Runtime.ConfigData.Identifiers;
using Code.Runtime.Entity.StatusEffects;
using Code.Runtime.Services.BattleService;
using UnityEngine;

namespace Code.Runtime.Modules.AbilitySystem.ActiveAbilities
{
    [CreateAssetMenu(fileName = "SpinAttackAbility", menuName = "AbilityData/Attack/SpinAttack")]
    public class SpinAttackBlueprint : ActiveAbilityBlueprint<SpinAttack>
    {
        public float Damage;

        public override ActiveAbility GetAbility()
        {
            //return new SpinAttack(Damage, _battleService);
            return null;
        }
    }

    public class SpinAttack : ActiveAbility
    {
        private readonly BattleService _battleService;

        public SpinAttack(IReadOnlyList<GameplayEffect> effects, AbilityIdentifier identifier, UnityEngine.Sprite icon,
            float cooldown, float activeTime, bool isCastAbility, BattleService battleService) : base(effects,
            identifier, icon, cooldown, activeTime, isCastAbility)
        {
            _battleService = battleService;
        }

        public override void Use(GameObject caster, GameObject target)
        {
        }
    }
}