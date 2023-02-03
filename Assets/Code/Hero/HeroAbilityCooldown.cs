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
                if (_abilitiesOnCooldown[i].CurrentCooldown <= 0)
                {
                    _abilitiesOnCooldown.RemoveAt(i);
                }
            }
        }

        public void StartCooldown(AbilityBluePrintBase ability)
        {
            if (!_abilitiesOnCooldown.Contains(ability))
            {
                ability.CurrentCooldown = ability.Cooldown;
                _abilitiesOnCooldown.Add(ability);
            }
        }
    }
}
