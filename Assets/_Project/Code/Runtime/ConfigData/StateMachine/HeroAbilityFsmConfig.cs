using System.Collections.Generic;
using UnityEngine;

namespace Code.Runtime.ConfigData.StateMachine
{
    [CreateAssetMenu(menuName = "Config/StateMachine/AbilityFsmDatabase", fileName = "AbilityFsmConfigDatabase")]
    public class HeroAbilityFsmConfig : ScriptableObject
    {
        public List<AbilityFsmConfig> AbilityFsmConfigs;
    }
}
