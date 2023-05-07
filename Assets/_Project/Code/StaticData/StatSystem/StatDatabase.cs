using System.Collections.Generic;
using UnityEngine;

namespace Code.StaticData.StatSystem
{
    [CreateAssetMenu(fileName = "StatDatabase", menuName = "StatSystem/StatDatabase")]
    public class StatDatabase : ScriptableObject
    {
        public List<StatDefinition> StatDefinitions = new List<StatDefinition>();
        public List<Attribute> AttributeDefinitions = new List<Attribute>();
    }
}
