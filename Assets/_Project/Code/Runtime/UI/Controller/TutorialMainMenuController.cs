﻿using System.Collections.Generic;
using System.Linq;
using Code.Runtime.ConfigData.Identifiers;
using Code.Runtime.Core.Audio;
using Code.Runtime.Core.SceneManagement;
using Code.Runtime.Modules.Advertisement;
using Code.Runtime.Modules.LocalizationProvider;
using Code.Runtime.Modules.TutorialService;
using Code.Runtime.Services.PauseService;
using Code.Runtime.Services.PersistentProgress;
using Code.Runtime.Services.SaveLoad;
using Code.Runtime.UI.Model;
using Code.Runtime.UI.Services;
using Code.Runtime.UI.View;
using UniRx;

namespace Code.Runtime.UI.Controller
{
    public class TutorialMainMenuController : MainMenuController
    {
        private readonly TutorialService _tutorialService;
        private List<TutorialElement> _tutorialElements = new();

        public TutorialMainMenuController(IGameDataContainer gameData, AdvertisementService adService,
            AudioService audioService, SaveLoadService saveLoad, PauseService pauseService,
            TutorialService tutorialService) : base(gameData, adService, audioService, saveLoad, pauseService)
        {
            _tutorialService = tutorialService;
        }

        public override void InitController(IScreenModel model, BaseWindowView windowView, ScreenService screenService)
        {
            base.InitController(model, windowView, screenService);

            _tutorialElements = _windowView.GetComponentsInChildren<TutorialElement>().ToList();

            foreach (var element in _tutorialElements)
            {
                element.OnClickAsObservable().Subscribe(HandleElementInteraction);
            }

            _tutorialService.OnElementHighlighted += HandleElementHighlighted;
            _tutorialService.StartTutorial();
        }

        public override void Dispose()
        {
            base.Dispose();
            _tutorialService.OnElementHighlighted -= HandleElementHighlighted;
        }


        private void HandleElementInteraction(TutorialElementIdentifier tutorialElement)
        {
            _tutorialService.HandleElementInteraction(tutorialElement.Name);
        }

        private void HandleElementHighlighted(TutorialElementIdentifier elementId)
        {
            foreach (var element in _tutorialElements)
            {
                if (element.GetId() == elementId)
                {
                    element.BlockInteractions(false);
                    element.Show(true);
                    _tutorialService.HandlePointer(element);
                }
                else
                {
                    if (!_tutorialService.IsPreviousStepElement(element.GetId()))
                    {
                        element.Show(false);
                    }

                    element.BlockInteractions(true);
                }
            }
        }
    }
}