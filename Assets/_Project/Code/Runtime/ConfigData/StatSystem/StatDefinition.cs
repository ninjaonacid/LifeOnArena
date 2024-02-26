using UnityEngine;

namespace Code.Runtime.ConfigData.StatSystem
{
    [CreateAssetMenu(fileName = "StatDefinition", menuName = "StatSystem/StatDefinition")]
    public class StatDefinition : ScriptableObject
    {
        [SerializeField] private int _baseValue;
        [SerializeField] private int _capacity;

        public int BaseValue => _baseValue;
        public int Capacity => _capacity;

    }
}
