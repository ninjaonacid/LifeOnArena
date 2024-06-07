using System.Collections.Generic;
using System.Linq;
using Code.Runtime.Core.Config;

namespace Code.Runtime.Modules.TutorialService
{
    public class TutorialService
    {
        private ConfigProvider _configProvider;
        private List<TutorialTask> _tutorialTasks = new();
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
            _tutorialTasks = tutorialConfig.TutorialTasks;
            _arrowPrefab = tutorialConfig.ArrowPrefab;
            _currentTask = _tutorialTasks[0];
        }
        
        public void UpdateTutorialStatus(TutorialTask task)
        {
            if (_tutorialTasks.Contains(task))
            {
                var tutorialStep = _tutorialTasks.FirstOrDefault(x => x == task);

                if (tutorialStep != null)
                {
                    
                }
            }
        }

        public TutorialTask GetCurrentTask()
        {
            return _currentTask;
        }
    }
}
