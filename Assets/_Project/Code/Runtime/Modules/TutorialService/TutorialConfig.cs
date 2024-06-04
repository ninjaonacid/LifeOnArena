using System.Collections.Generic;
using UnityEngine;

namespace Code.Runtime.Modules.TutorialService
{
    [CreateAssetMenu(menuName = "Config/Tutorial/TutorialConfig", fileName = "TutorialConfig")]
    public class TutorialConfig : ScriptableObject
    {
        [SerializeField] private List<TutorialTask> _tutorialTasks;

        public List<TutorialTask> TutorialTasks => _tutorialTasks;
    }
}