using System;
using System.Collections.Generic;

namespace Code.Runtime.Data.PlayerData
{
    [Serializable]
    public class PassiveSkills
    {
        public List<string> AbilitiesId;

        public PassiveSkills()
        {
            AbilitiesId = new List<string>();
        }
    }
}