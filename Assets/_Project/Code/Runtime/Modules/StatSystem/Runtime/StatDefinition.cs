using UnityEngine;

namespace Code.Runtime.Modules.StatSystem
{
    [CreateAssetMenu(fileName = "StatDefinition", menuName = "StatSystem/StatDefinition")]
    public class StatDefinition : ScriptableObject
    {
        [SerializeField] private int _baseValue;
        [SerializeField] private int _capacity;
        [SerializeField] private int _statPerLevel;
        public int BaseValue => _baseValue;
        public int Capacity => _capacity;
        public int StatPerLevel => _statPerLevel;

    }
}
