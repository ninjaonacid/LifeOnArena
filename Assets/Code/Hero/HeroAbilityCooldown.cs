using System.Collections.Generic;
using Code.StaticData.Ability;
using UnityEngine;

namespace Code.Hero
{
    public class HeroAbilityCooldown : MonoBehaviour
    {
        private readonly List<AbilityBluePrintBase> _abilitiesOnCooldown = new List<AbilityBluePrintBase>();


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

        public void StartCooldown(AbilityBluePrintBase ability)
        {
            if (!_abilitiesOnCooldown.Contains(ability))
            {
                ability.CurrentActiveTime = ability.ActiveTime;   
                ability.CurrentCooldown = ability.Cooldown;
                _abilitiesOnCooldown.Add(ability);
            }
        }
    }
}
