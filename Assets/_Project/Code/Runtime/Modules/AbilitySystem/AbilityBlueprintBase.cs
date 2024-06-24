using System.Collections.Generic;
using Code.Runtime.ConfigData.Identifiers;
using Code.Runtime.Data.PlayerData;
using Code.Runtime.Modules.Requirements;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Localization;

namespace Code.Runtime.Modules.AbilitySystem
{
    public class AbilityBlueprintBase : SerializedScriptableObject
    {
        [SerializeField] private AbilityIdentifier _identifier;
        [SerializeField] private Sprite _icon;
        [SerializeField] private LocalizedString _description;
        [SerializeField] private AbilityTreeData _treeData;
        public AbilityIdentifier Identifier => _identifier;
        public Sprite Icon => _icon;
        public LocalizedString Description => _description;
        public AbilityTreeData AbilityTreeData => _treeData;
    }
}
