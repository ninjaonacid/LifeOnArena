using System.Linq;
using Code.Runtime.ConfigData.Identifiers;
using Code.Runtime.Data.PlayerData;
using UnityEngine;

namespace Code.Runtime.Modules.Requirements
{
    public class PreviousAbilityRequirement : IRequirement<PlayerData>
    {
        [SerializeField] private AbilityIdentifier _requiredAbility;
        
        public bool CheckRequirement(PlayerData value)
        {
           return value.AbilityData.Abilities
               .Any(x => x.AbilityId == _requiredAbility.Id);
        }
    }
}