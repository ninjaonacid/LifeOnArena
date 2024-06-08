using System.Collections.Generic;
using System.Linq;
using Code.Runtime.ConfigData.Identifiers;
using Code.Runtime.Modules.TutorialService;
using Code.Runtime.Services.SaveLoad;
using Code.Runtime.UI.Model;
using Code.Runtime.UI.Services;
using Code.Runtime.UI.View;
using UniRx;

namespace Code.Runtime.UI.Controller
{
    public class TutorialAbilityScreenController : AbilityScreenController
    {
        private TutorialService _tutorialService;
        private List<TutorialElement> _tutorialElements = new();
 
        public TutorialAbilityScreenController(ISaveLoadService saveLoad, TutorialService tutorialService) : base(saveLoad)
        {
            _tutorialService = tutorialService;
        }

        public override void InitController(IScreenModel model, BaseWindowView windowView, ScreenService screenService)
        {
            base.InitController(model, windowView, screenService);
            
            _tutorialElements = _screenView.GetComponentsInChildren<TutorialElement>().ToList();

            foreach (var element in _tutorialElements)
            {
                element.OnClickAsObservable().Subscribe(HandleTutorialLogic);
            }
            
            _tutorialService.OnTaskChanged += UpdateTutorial;

        }
        
        private void HandleTutorialLogic(TutorialElementIdentifier tutorialElement)
        {
            var task = _tutorialService.GetCurrentTask();

            if (tutorialElement.Id == task.ElementId.Id)
            {
                _tutorialService.UpdateTutorialStatus(task);
            }
        }

        private void UpdateTutorial(TutorialTask task)
        {
            TutorialElement currentElement = default;
            
            _tutorialElements.ForEach(x =>
            {
                if (x.GetId() == task.ElementId)
                {
                    currentElement = x;
                    currentElement.BlockInteractions(false);
                    _tutorialService.HandlePointer(currentElement);
                }
                else
                {
                    x.BlockInteractions(true);
                }
            });

        }
    }
}