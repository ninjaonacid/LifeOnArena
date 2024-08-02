using System.Collections.Generic;
using System.Linq;
using Code.Runtime.ConfigData.Identifiers;
using Code.Runtime.Core.Audio;
using Code.Runtime.Core.Factory;
using Code.Runtime.Data.PlayerData;
using Code.Runtime.Modules.TutorialService;
using Code.Runtime.Services.SaveLoad;
using Code.Runtime.UI.Model;
using Code.Runtime.UI.Services;
using Code.Runtime.UI.View;
using UniRx;

namespace Code.Runtime.UI.Controller
{
    public class TutorialWeaponScreenController : WeaponScreenController
    {
        private readonly TutorialService _tutorialService;
        private List<TutorialElement> _tutorialElements = new();

        public TutorialWeaponScreenController(AudioService audioService, HeroFactory heroFactory,
            SaveLoadService saveLoad, PlayerData playerData, TutorialService tutorialService) : base(audioService,
            heroFactory, saveLoad, playerData)
        {
            _tutorialService = tutorialService;
        }

        public override void InitController(IScreenModel model, BaseWindowView windowView, ScreenService screenService)
        {
            base.InitController(model, windowView, screenService);

            _tutorialElements = _windowView.GetComponentsInChildren<TutorialElement>().ToList();

            foreach (var element in _tutorialElements)
            {
                element.OnClickAsObservable().Subscribe(HandleElementInteraction).AddTo(_windowView);
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