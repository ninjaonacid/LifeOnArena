using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Code.Runtime.Modules.Requirements
{
    [CreateAssetMenu(menuName = "Config/Requirements", fileName = "RequirementsConfig")] 
    public class RequirementConfig : SerializedScriptableObject
    {
        // [SerializeField] private List<IRequirement> _requirements;
        // public List<IRequirement> Requirements => _requirements;
    }
}
