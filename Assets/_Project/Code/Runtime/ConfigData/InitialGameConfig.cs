using Code.Runtime.ConfigData.Identifiers;
using Code.Runtime.Modules.StatSystem;
using UnityEngine;

namespace Code.Runtime.ConfigData
{
    [CreateAssetMenu(fileName = "GameConfig", menuName = "Config/InitialGameConfig")]
    public class InitialGameConfig : ScriptableObject
    {
        [SerializeField] private StatDatabase _characterStats;
        [SerializeField] private int _statUpgradePrice;
        [SerializeField] private WeaponId _startWeapon;
        [SerializeField] private bool _isNeedTutorial;

        public StatDatabase CharacterStats => _characterStats;
        public int StatUpgradePrice => _statUpgradePrice;
        public WeaponId StartWeapon => _startWeapon;
        public bool IsNeedTutorial => _isNeedTutorial;
    }
}