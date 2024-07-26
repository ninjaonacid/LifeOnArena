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
            _comboWindow = attackConfig.AnimationData.Length;

            ActiveTime = attackConfig.ExitTime;
            CurrentActiveTime = attackConfig.ExitTime;
            
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