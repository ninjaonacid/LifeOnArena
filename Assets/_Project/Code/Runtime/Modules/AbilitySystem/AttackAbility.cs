using System.Collections.Generic;
using Code.Runtime.ConfigData.Attack;
using Code.Runtime.ConfigData.Weapon;
using Code.Runtime.Entity;
using Code.Runtime.Logic.Timer;
using UnityEngine;

namespace Code.Runtime.Modules.AbilitySystem
{
    public class AttackAbility : ActiveAbility
    {
        private List<AttackConfig> _attackConfigs;
        private ITimer _timer;
        public int ComboCount { get; set; } = 0;
        private float _lastAttackTime = 0f;
        private float _comboWindow = 0.5f;

        public AttackAbility(ActiveAbilityBlueprintBase abilityBlueprint) : base(abilityBlueprint)
        {
            _timer = new Timer();
        }

        public override void Use(AbilityController caster, GameObject target)
        {
            WeaponData weaponData = caster.GetComponent<EntityWeapon>().WeaponData;
            
            if (ComboCount >= weaponData.AttacksConfigs.Count)
            {
                ResetComboCounter();
            }
            
            AttackConfig attackConfig = default;
            attackConfig = weaponData.AttacksConfigs[ComboCount];
            ActiveTime = attackConfig.AnimationData.Length - 1f;
        
            
            
            ComboCount++;
        }

        public void ResetComboCounter()
        {
            ComboCount = 0;
        }
    }
}