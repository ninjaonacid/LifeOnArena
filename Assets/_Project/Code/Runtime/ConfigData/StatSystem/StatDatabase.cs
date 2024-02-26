using System.Collections.Generic;
using UnityEngine;

namespace Code.Runtime.ConfigData.StatSystem
{
    [CreateAssetMenu(fileName = "StatDatabase", menuName = "StatSystem/StatDatabase")]
    public class StatDatabase : ScriptableObject
    {
        public List<StatDefinition> StatDefinitions = new List<StatDefinition>();
        public List<StatDefinition> AttributeDefinitions = new List<StatDefinition>();
        public List<StatDefinition> PrimaryStats = new List<StatDefinition>();
    }
}
