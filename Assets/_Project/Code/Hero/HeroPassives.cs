using System.Collections.Generic;
using Code.Data;
using Code.Services.PersistentProgress;
using Code.StaticData.Ability.PassiveAbilities;
using UnityEngine;

namespace Code.Hero
{
    public class HeroPassives : MonoBehaviour, ISave
    {
        private PlayerProgress _progress;
        public List<PassiveAbilityTemplateBase> _heroPassives;



        public void AddPassive(PassiveAbilityTemplateBase passiveAbility)
        {
            if (_heroPassives.Contains(passiveAbility))
            {
                passiveAbility.GetAbility().Apply(gameObject, _progress );
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
        }

        public void UpdateProgress(PlayerProgress progress)
        {
            throw new System.NotImplementedException();
        }
    }
}
