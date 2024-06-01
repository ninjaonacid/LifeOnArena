using System;
using UnityEngine;

namespace Code.Runtime.Modules.TutorialService
{
    public class TutorialTask : ScriptableObject
    {
        [SerializeField] private string ElementId;
        
        public event Action Completed;
    }
}