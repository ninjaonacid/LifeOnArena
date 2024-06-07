using System.Collections.Generic;
using System.Linq;
using Code.Runtime.Core.Config;
using Code.Runtime.UI;
using UnityEngine;

namespace Code.Runtime.Modules.TutorialService
{
    public class TutorialService
    {
        private ConfigProvider _configProvider;
        private Queue<TutorialTask> _tutorialTasks = new();
        private List<TutorialTask> _completedTasks = new();
        private TutorialTask _currentTask;
        private ArrowUI _arrowPrefab;

        public TutorialService(ConfigProvider configProvider)
        {
            _configProvider = configProvider;
            Initialize();
        }

        private void Initialize()
        {
            var tutorialConfig = _configProvider.GetTutorialConfig();
            _tutorialTasks = new Queue<TutorialTask>(tutorialConfig.TutorialTasks);
            _arrowPrefab = tutorialConfig.ArrowPrefab;
            _currentTask = _tutorialTasks.Peek();
        }
        
        public void UpdateTutorialStatus(TutorialTask task)
        {
            _completedTasks.Add(_currentTask);
            _tutorialTasks.Dequeue();
            _currentTask = _tutorialTasks.Peek();
            Debug.Log($"Current task is {_currentTask}");
        }

        public TutorialTask GetCurrentTask()
        {
            return _currentTask;
        }

        public ArrowUI GetArrowUI()
        {
            return _arrowPrefab;
        }
    }
}
