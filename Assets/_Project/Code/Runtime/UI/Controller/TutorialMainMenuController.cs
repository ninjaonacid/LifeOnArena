using System;
using System.Linq;
using Code.Runtime.ConfigData.Identifiers;
using Code.Runtime.Core.Audio;
using Code.Runtime.Core.SceneManagement;
using Code.Runtime.Modules.TutorialService;
using Code.Runtime.Services.PersistentProgress;
using Code.Runtime.UI.Model;
using Code.Runtime.UI.Services;
using Code.Runtime.UI.View;
using Sirenix.Utilities;
using UniRx;

namespace Code.Runtime.UI.Controller
{
    public class TutorialMainMenuController : MainMenuController
    {
        private TutorialService _tutorialService;

        public TutorialMainMenuController(IGameDataContainer gameData, AudioService audioService,
            SceneLoader sceneLoader, TutorialService tutorialService) : base(gameData, audioService, sceneLoader)
        {
            _tutorialService = tutorialService;
        }

        public override void InitController(IScreenModel model, BaseWindowView windowView, ScreenService screenService)
        {
            base.InitController(model, windowView, screenService);

            var tutorialElements = _windowView.GetComponentsInChildren<TutorialElement>();

            foreach (var element in tutorialElements)
            {
                element.OnClickAsObservable().Subscribe(HandleTutorialLogic);
            }

            TutorialTask task = _tutorialService.GetCurrentTask();

            tutorialElements.Where(x => x.GetId() != task.ElementId).ForEach(x => x.BlockInteractions(true));
            
            
        }

        private void HandleTutorialLogic(TutorialElementIdentifier tutorialElement)
        {
            var task = _tutorialService.GetCurrentTask();

            if (tutorialElement.Id == task.ElementId.Id)
            {
                //_tutorialService.UpdateTutorialStatus();
            }
        }
    }
}