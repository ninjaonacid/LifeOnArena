using System.Collections.Generic;
using Code.Data;
using Code.Infrastructure.Factory;
using Code.Services.PersistentProgress;
using Code.StaticData.Ability.PassiveAbilities;
using UnityEngine;
using VContainer;

namespace Code.Entity.Hero
{
    public class HeroPassives : MonoBehaviour, ISave
    {
        private PlayerData _data;
        private List<PassiveAbilityTemplateBase> _heroPassives = new List<PassiveAbilityTemplateBase>();
        private IAbilityFactory _abilityFactory;

        [Inject]
        private void Construct(IAbilityFactory abilityFactory)
        {
            _abilityFactory = abilityFactory;
        }
        public void AddPassive(PassiveAbilityTemplateBase passiveAbility)
        {
            if (_heroPassives.Contains(passiveAbility))
            {
                passiveAbility.GetAbility().Apply(gameObject, _data);
            }
            else
            {
                _heroPassives.Add(passiveAbility);
                passiveAbility.GetAbility().Apply(gameObject, _data);
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
