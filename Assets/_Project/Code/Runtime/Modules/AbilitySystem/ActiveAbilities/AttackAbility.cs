using System.Collections.Generic;
using Code.Runtime.ConfigData.Attack;
using Code.Runtime.ConfigData.Weapon;
using Code.Runtime.Entity;
using Code.Runtime.Logic.Timer;
using UnityEngine;

namespace Code.Runtime.Modules.AbilitySystem.ActiveAbilities
{
    public class AttackAbility : ActiveAbility
    {
        private List<AttackConfig> _attackConfigs;
        private readonly ITimer _timer;
        public int ComboCount { get; set; } = 0;
        public int MaxCombo { get; private set; } = 3;
        private float _comboWindow;

        public override bool CanCombo => ComboCount < MaxCombo &&
                                         State == AbilityState.Active &&
                                         _timer.Elapsed <= _comboWindow;

        public AttackAbility(ActiveAbilityBlueprintBase abilityBlueprint) : base(abilityBlueprint)
        {
            _timer = new Timer();
        }

        public override void Use(AbilityController caster, GameObject target)
        {
            WeaponData weaponData = caster.GetComponent<EntityWeapon>().WeaponData;

            MaxCombo = weaponData.AttacksConfigs.Count;

            if (_timer.Elapsed > _comboWindow)
            {
                ResetComboCounter();
            }

            AttackConfig attackConfig = default;
            attackConfig = weaponData.AttacksConfigs[ComboCount];
            float attackAnimationLength = attackConfig.AnimationData.Length;
            float attackAnimationSpeed = attackConfig.AttackAnimationSpeed;

            _comboWindow = attackAnimationLength / attackAnimationSpeed;

            ActiveTime = attackAnimationLength / attackAnimationSpeed - 0.2f;

            CurrentActiveTime = attackAnimationLength / attackAnimationSpeed - 0.2f;

            _audioService.PlaySound3D(weaponData.WeaponSound, caster.transform);
            ComboCount++;

            _timer.Reset();
        }

        public void ResetComboCounter()
        {
            ComboCount = 0;
        }
    }
}