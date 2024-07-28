using System;
using Code.Runtime.ConfigData.Identifiers;
using UnityEngine;

namespace Code.Runtime.Modules.TutorialService
{
    [CreateAssetMenu(menuName = "Config/Tutorial/TutorialTask", fileName = "TutorialTask")]
    public class TutorialTask : ScriptableObject
    {
        [SerializeField] private TutorialTaskData _data;
        
        [SerializeField] private TutorialElementIdentifier _elementId;
        
        public TutorialTaskData TaskData => _data;
        public TutorialElementIdentifier ElementId => _elementId;

    }
}