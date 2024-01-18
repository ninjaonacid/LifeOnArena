using System.Collections.Generic;
using UnityEngine;

namespace Code.ConfigData.StateMachine
{
    [CreateAssetMenu(menuName = "Config/StateMachine/AbilityFsmDatabase", fileName = "AbilityFsmConfigDatabase")]
    public class HeroAbilityFsmConfig : ScriptableObject
    {
        public List<AbilityFsmConfig> AbilityFsmConfigs;
    }
}
