using System;
using UnityEngine;

namespace Code.Runtime.Modules.TutorialService
{
    [Serializable]
    public class TutorialTaskData
    {
        public Vector2 TutorialArrowPosition;
        public float ZRotation;
        public string Description;
        public bool IsCompleted;
    }
}
