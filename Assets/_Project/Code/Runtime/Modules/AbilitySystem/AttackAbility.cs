﻿using System.Collections.Generic;
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
        private readonly ITimer _timer;
        public int ComboCount { get; private set; } = 0;
        public int MaxCombo { get; private set; } = 0;
        private float _lastAttackTime = 0f;
        private float _comboWindow;

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
            attackConfig = weaponData.AttacksConfigs[ComboCount % weaponData.AttacksConfigs.Count];
            _comboWindow = attackConfig.AnimationData.Length + 0.2f;

            ActiveTime = attackConfig.AnimationData.Length - 0.1f;
            CurrentActiveTime = attackConfig.AnimationData.Length;
            
            Debug.Log("ATTACK ABILITY ACTIVATED");

            ComboCount++;
            
            _timer.Reset();
        }

        public void ResetComboCounter()
        {
            ComboCount = 0;
        }
        
    }
}