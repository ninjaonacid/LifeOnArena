using System.Collections.Generic;
using Code.Runtime.Core.Factory;
using Code.Runtime.Data.PlayerData;
using Code.Runtime.Modules.AbilitySystem.PassiveAbilities;
using Code.Runtime.Services.PersistentProgress;
using UnityEngine;

namespace Code.Runtime.Entity.Hero
{
    public class HeroPassives : MonoBehaviour, ISave
    {
        private PlayerData _data;
        private List<PassiveAbilityBlueprintTemplateBase> _heroPassives = new List<PassiveAbilityBlueprintTemplateBase>();
        private IAbilityFactory _abilityFactory;

       
        public void AddPassive(PassiveAbilityBlueprintTemplateBase passiveAbilityBlueprint)
        {
            if (_heroPassives.Contains(passiveAbilityBlueprint))
            {
                passiveAbilityBlueprint.GetAbility().Apply(gameObject, _data);
            }
            else
            {
                _heroPassives.Add(passiveAbilityBlueprint);
                passiveAbilityBlueprint.GetAbility().Apply(gameObject, _data);
            }
        }

        public void LoadData(PlayerData data)
        {
            _data = data;
            foreach (var id in _data.PassiveSkills.AbilitiesId)
            {
               // AddPassive(_abilityFactory.CreatePassive(id));
            }
            
        }

        public void UpdateData(PlayerData data)
        {
           
        }
    }
}
