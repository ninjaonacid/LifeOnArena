using System.Collections.Generic;
using Code.Data;
using Code.Infrastructure.Factory;
using Code.Services.PersistentProgress;
using Code.StaticData.Ability.PassiveAbilities;
using UnityEngine;
using VContainer;

namespace Code.Hero
{
    public class HeroPassives : MonoBehaviour, ISave
    {
        private PlayerProgress _progress;
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
                passiveAbility.GetAbility().Apply(gameObject, _progress);
            }
            else
            {
                _heroPassives.Add(passiveAbility);
                passiveAbility.GetAbility().Apply(gameObject, _progress);
            }
        }

        public void LoadProgress(PlayerProgress progress)
        {
            _progress = progress;
            foreach (var id in _progress.PassiveSkills.AbilitiesId)
            {
               // AddPassive(_abilityFactory.CreatePassive(id));
            }
            
        }

        public void UpdateProgress(PlayerProgress progress)
        {
           
        }
    }
}
