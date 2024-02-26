using System.Collections.Generic;
using UnityEngine;

namespace Code.Runtime.ConfigData.StateMachine
{
    [CreateAssetMenu(menuName = "Config/StateMachine/WeaponConfigsDatabase", fileName = "WeaponFsmConfigDatabase")]
    public class WeaponStateMachineDatabase : ScriptableObject
    {
        public List<WeaponFsmConfig> WeaponConfigs;
    }
}
