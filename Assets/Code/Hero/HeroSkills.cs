using System.Collections.Generic;
using Code.StaticData.Ability;
using UnityEngine;

namespace Code.Hero
{
    public class HeroSkills : MonoBehaviour
    {
        public List<HeroAbility_SO> HeroAbilities = new List<HeroAbility_SO>();

        private Dictionary<SimpleInput.ButtonInput, HeroAbility_SO> skillBinds;
    }
}
