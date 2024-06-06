using System.Collections.Generic;
using Code.Runtime.ConfigData.Identifiers;
using Code.Runtime.Data.PlayerData;
using Code.Runtime.Modules.Requirements;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Code.Runtime.Modules.AbilitySystem
{
    public class AbilityBlueprintBase : SerializedScriptableObject
    {
        [SerializeField] private AbilityIdentifier _identifier;
        [SerializeField] private Sprite _icon;
        [SerializeField] private string _description;
        [SerializeField] private AbilityTreeData _treeData;
        public AbilityIdentifier Identifier => _identifier;
        public Sprite Icon => _icon;
        public string Description => _description;
        public AbilityTreeData AbilityTreeData => _treeData;
    }
}
