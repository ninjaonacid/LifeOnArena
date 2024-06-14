using System.Collections.Generic;
using Code.Runtime.ConfigData.Attack;
using Code.Runtime.ConfigData.Weapon;
using Code.Runtime.Entity;
using UnityEngine;

namespace Code.Runtime.Modules.AbilitySystem
{
    public class CommonAttackAbility : ActiveAbility
    {
        private int _attackNumber;
        private List<AttackConfig> _attackConfigs;

        public CommonAttackAbility(ActiveAbilityBlueprintBase abilityBlueprint) : base(abilityBlueprint)
        {
        }

        public override void Use(AbilityController caster, GameObject target)
        {
            WeaponData weaponData = caster.GetComponent<EntityWeapon>().GetEquippedWeaponData();

            AttackConfig attackConfig = default;
            
            attackConfig = weaponData.AttacksConfigs[_attackNumber % weaponData.AttacksConfigs.Count];
            ActiveTime = attackConfig.AnimationData.Length;
            CurrentActiveTime = attackConfig.AnimationData.Length;
            _attackNumber++;
            
        }
    }
}