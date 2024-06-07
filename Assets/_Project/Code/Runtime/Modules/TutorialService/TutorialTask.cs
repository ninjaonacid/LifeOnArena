using System;
using Code.Runtime.ConfigData.Identifiers;
using UnityEngine;

namespace Code.Runtime.Modules.TutorialService
{
    [CreateAssetMenu(menuName = "Config/Tutorial/TutorialTask", fileName = "TutorialTask")]
    public class TutorialTask : ScriptableObject
    {
        public event Action Completed;
        public event Action TaskStarted;
        
        [SerializeField] private TutorialElementIdentifier _elementId;
        [SerializeField] private string _description;

        public TutorialElementIdentifier ElementId => _elementId;
        public string Description => _description;
        
    }
}