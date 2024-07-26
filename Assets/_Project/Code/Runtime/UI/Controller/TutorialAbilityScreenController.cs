﻿using System.Collections.Generic;
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
        private readonly TutorialService _tutorialService;
        private List<TutorialElement> _tutorialElements = new();
 
        public TutorialAbilityScreenController(SaveLoadService saveLoad, TutorialService tutorialService) : base(saveLoad)
        {
            _tutorialService = tutorialService;
        }

        public override void InitController(IScreenModel model, BaseWindowView windowView, ScreenService screenService)
        {
            base.InitController(model, windowView, screenService);
            
            _tutorialElements = _screenView.GetComponentsInChildren<TutorialElement>().ToList();

            foreach (var element in _tutorialElements)
            {
                element.OnClickAsObservable().Subscribe(HandleElementInteraction).AddTo(_screenView);
            }
            
            _tutorialService.OnElementHighlighted += HandleElementHighlighted;

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
                    _tutorialService.HandlePointer(element);
                }
                else
                {
                    element.BlockInteractions(true);
                }
            }
        }
        
    }
}