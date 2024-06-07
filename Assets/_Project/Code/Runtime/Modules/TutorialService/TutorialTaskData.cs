using System;
using UnityEngine;

namespace Code.Runtime.Modules.TutorialService
{
    [Serializable]
    public class TutorialTaskData
    {
        public Vector3 TutorialArrowPosition;
        public float ZRotation;
        public float MovementOffset = 100;
        public string Description;
        public bool IsCompleted;
    }
}
