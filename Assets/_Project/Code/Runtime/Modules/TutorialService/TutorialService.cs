using System.Collections.Generic;
using System.Linq;
using Code.Runtime.Core.ConfigProvider;

namespace Code.Runtime.Modules.TutorialService
{
    public class TutorialService
    {
        private ConfigProvider _configProvider;
        private List<TutorialTask> _tutorialTasks = new();
        private TutorialTask _currentTask;

        public TutorialService(ConfigProvider configProvider)
        {
            _configProvider = configProvider;
        }

        private void Initialize()
        {
            
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
