using System.Collections.Generic;
using UnityEngine;

namespace Code.ConfigData.StateMachine
{
    [CreateAssetMenu(menuName = "Config/StateMachine/WeaponConfigsDatabase", fileName = "WeaponFsmConfigDatabase")]
    public class WeaponStateMachineDatabase : ScriptableObject
    {
        public List<WeaponFsmConfig> WeaponConfigs;
    }
}
