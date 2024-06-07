using System.Collections.Generic;
using UnityEngine;

namespace Code.Runtime.Modules.TutorialService
{
    [CreateAssetMenu(menuName = "Config/Tutorial/TutorialConfig", fileName = "TutorialConfig")]
    public class TutorialConfig : ScriptableObject
    {
        [SerializeField] private ArrowUI _arrowPrefab;
        
        [SerializeField] private List<TutorialTask> _tutorialTasks;
        public ArrowUI ArrowPrefab => _arrowPrefab;
        public List<TutorialTask> TutorialTasks => _tutorialTasks;
    }
}