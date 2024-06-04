using System;
using UnityEngine;

namespace Code.Runtime.Modules.TutorialService
{
    [CreateAssetMenu(menuName = "Config/Tutorial/TutorialTask", fileName = "TutorialTask")]
    public class TutorialTask : ScriptableObject
    {
        [SerializeField] private string ElementId;
        
        public event Action Completed;
    }
}