using System.Collections.Generic;
using Code.Runtime.ConfigData.Attack;
using Code.Runtime.ConfigData.Weapon;
using Code.Runtime.Entity;
using Cysharp.Threading.Tasks;
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
            VisualEffectController vfxController = caster.GetComponent<VisualEffectController>();

            AttackConfig attackConfig = default;

            if (caster.ActiveAbility == null)
            {
                _attackNumber = 0;
                attackConfig = weaponData.AttacksConfigs[_attackNumber];
                ActiveTime = attackConfig.AnimationData.Length;
                CurrentActiveTime = attackConfig.AnimationData.Length;
                _attackNumber++;
            }
            
            if (caster.ActiveAbility != null && caster.ActiveAbility == this)
            {
                attackConfig = weaponData.AttacksConfigs[_attackNumber];
                ActiveTime = attackConfig.AnimationData.Length;
                CurrentActiveTime = attackConfig.AnimationData.Length;
                _attackNumber++;
            }

            if (attackConfig != null)
            {
                vfxController.PlaySlashVisualEffect(
                    attackConfig.SlashConfig.VisualEffect.Identifier, attackConfig.SlashDirection, 
                    attackConfig.SlashConfig.SlashSize, attackConfig.SlashDelay).Forget();
            }
            
               
           
            
        
        }
    }
}