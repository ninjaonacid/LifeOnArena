using System.Collections.Generic;
using System.Linq;

namespace Code.Runtime.Modules.TutorialService
{
    public class TutorialService
    {
        private List<TutorialTask> _tutorialSteps = new();


        public void UpdateTutorialStatus(TutorialTask task)
        {
            if (_tutorialSteps.Contains(task))
            {
                var tutorialStep = _tutorialSteps.FirstOrDefault(x => x == task);

                if (tutorialStep != null)
                {
                    
                }
            }
        }
    }
}
