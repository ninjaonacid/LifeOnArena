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
        private float _comboWindow;

        public AttackAbility(ActiveAbilityBlueprintBase abilityBlueprint) : base(abilityBlueprint)
        {
            _timer = new Timer();
        }

        public override void Use(AbilityController caster, GameObject target)
        {
            WeaponData weaponData = caster.GetComponent<EntityWeapon>().WeaponData;

            if (Time.time - _lastAttackTime > _comboWindow)
            {
                ResetComboCounter();
            }

            AttackConfig attackConfig = default;
            attackConfig = weaponData.AttacksConfigs[ComboCount % weaponData.AttacksConfigs.Count];
            _comboWindow = attackConfig.AnimationData.Length;

            ActiveTime = attackConfig.AnimationData.Length - 0.5f;
            CurrentActiveTime = attackConfig.AnimationData.Length;

            ComboCount++;
            _lastAttackTime = Time.time;
        }

        public void ResetComboCounter()
        {
            ComboCount = 0;
        }
    }
}