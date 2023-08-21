using System;
using System.Collections.Generic;
using Code.UI.HUD.Skills;

namespace Code.Data
{
    [Serializable]
    public class SkillSlotsData
    {
        public Queue<int> SkillIds = new Queue<int>();
        public SkillSlotsData()
        {
        }
        
    }

}