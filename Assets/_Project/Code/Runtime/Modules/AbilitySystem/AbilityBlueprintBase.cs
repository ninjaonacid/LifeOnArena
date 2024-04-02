using Code.Runtime.ConfigData.Identifiers;
using UnityEngine;

namespace Code.Runtime.Modules.AbilitySystem
{
    public class AbilityBlueprintBase : ScriptableObject
    {
        [SerializeField] private AbilityIdentifier _identifier;
        [SerializeField] private Sprite _icon;
        [SerializeField] private AbilityTreeData _treeData;

        public AbilityIdentifier Identifier => _identifier;
        public Sprite Icon => _icon;
        public AbilityTreeData AbilityTreeData => _treeData;
    }
}
