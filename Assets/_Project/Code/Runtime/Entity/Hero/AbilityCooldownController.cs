using System.Collections.Generic;
using Code.Runtime.Modules.AbilitySystem;
using UnityEngine;

namespace Code.Runtime.Entity.Hero
{
    public class AbilityCooldownController : MonoBehaviour
    {
        private readonly List<ActiveAbilityBlueprintBase> _abilitiesOnCooldown = new List<ActiveAbilityBlueprintBase>();

        private void Update()
        {
            for (int i = 0; i < _abilitiesOnCooldown.Count; i++)
            {
                _abilitiesOnCooldown[i].CurrentCooldown -= Time.deltaTime;
                _abilitiesOnCooldown[i].CurrentActiveTime -= Time.deltaTime;

                if (_abilitiesOnCooldown[i].CurrentActiveTime <= 0)
                {
                    _abilitiesOnCooldown[i].State = AbilityState.Cooldown;
                }
                if (_abilitiesOnCooldown[i].CurrentCooldown <= 0)
                {
                    _abilitiesOnCooldown[i].State = AbilityState.Ready;
                    _abilitiesOnCooldown.RemoveAt(i);
                }
            }
        }

        public void StartCooldown(ActiveAbilityBlueprintBase activeAbilityBlueprintBase)
        {
            if (!_abilitiesOnCooldown.Contains(activeAbilityBlueprintBase))
            {
                activeAbilityBlueprintBase.CurrentActiveTime = activeAbilityBlueprintBase.ActiveTime;   
                activeAbilityBlueprintBase.CurrentCooldown = activeAbilityBlueprintBase.Cooldown;
                _abilitiesOnCooldown.Add(activeAbilityBlueprintBase);
            }
        }

        public bool IsOnCooldown(ActiveAbilityBlueprintBase activeAbilityBlueprintBase)
        {
            return _abilitiesOnCooldown.Contains(activeAbilityBlueprintBase);
        }
    }
}
