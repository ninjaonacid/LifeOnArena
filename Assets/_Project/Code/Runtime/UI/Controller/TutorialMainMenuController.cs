﻿using System;
using System.Collections.Generic;
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
using UnityEngine;

namespace Code.Runtime.UI.Controller
{
    public class TutorialMainMenuController : MainMenuController
    {
        private TutorialService _tutorialService;
        private List<TutorialElement> _tutorialElements = new();
        private ArrowUI _arrow;

        public TutorialMainMenuController(IGameDataContainer gameData, AudioService audioService,
            SceneLoader sceneLoader, TutorialService tutorialService) : base(gameData, audioService, sceneLoader)
        {
            _tutorialService = tutorialService;
        }

        public override void InitController(IScreenModel model, BaseWindowView windowView, ScreenService screenService)
        {
            base.InitController(model, windowView, screenService);

            _tutorialElements = _windowView.GetComponentsInChildren<TutorialElement>().ToList();

            foreach (var element in _tutorialElements)
            {
                element.OnClickAsObservable().Subscribe(HandleTutorialLogic);
            }

            if (_arrow == null)
            {
                _arrow = UnityEngine.Object.Instantiate(_tutorialService.GetArrowUI());
                _arrow.gameObject.SetActive(false);
            }
            
            
            UpdateTutorial();
        }

        private void HandleTutorialLogic(TutorialElementIdentifier tutorialElement)
        {
            var task = _tutorialService.GetCurrentTask();

            if (tutorialElement.Id == task.ElementId.Id)
            {
                _tutorialService.UpdateTutorialStatus(task);
            }
            
            UpdateTutorial();
        }

        private void UpdateTutorial()
        {
            var task = _tutorialService.GetCurrentTask();

            _tutorialElements.Where(x => x.GetId() != task.ElementId).ForEach(x => x.BlockInteractions(true));
            var currentElement = _tutorialElements.FirstOrDefault(x => x.GetId() == task.ElementId);

            if (currentElement != null)
            {
                _arrow.gameObject.SetActive(true);
                
                RectTransform transform;
                (transform = (RectTransform)_arrow.transform).SetParent(currentElement.transform);
                
                transform.localScale = Vector3.one;
                transform.anchoredPosition = task.TaskData.TutorialArrowPosition;
                var transformLocalRotation = transform.localRotation;
                var eulerAngles = transformLocalRotation.eulerAngles;
                eulerAngles.z = task.TaskData.ZRotation;
                transform.localRotation = Quaternion.Euler(eulerAngles);

                var movementDistance = 50f;
                Vector2 forwardDirection = transform.up;

                Vector2 newPosition =
                    (Vector2)transform.anchoredPosition + forwardDirection * movementDistance;
                
                _arrow.Movement(newPosition); }

        }
        
    }
}