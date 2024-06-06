using System;
using System.Collections.Generic;
using Code.Runtime.UI.Model.AbilityMenu;

namespace Code.Runtime.Data.PlayerData
{
    [Serializable]
    public class AbilityData
    {
        public List<AbilityModel> Abilities = new();
        public List<AbilityModel> EquippedAbilities = new();

    }

}