using System;
using System.Collections.Generic;
using System.Linq;
using Code.Runtime.UI.Controller;
using Code.Runtime.UI.Model;
using Code.Runtime.UI.Model.AbilityMenu;
using Code.Runtime.UI.Model.ArenaSelectionScreenModel;
using Code.Runtime.UI.Model.DTO;
using Code.Runtime.UI.Model.MissionSummaryWindowModel;
using Code.Runtime.UI.Model.WeaponScreen;
using Code.Runtime.UI.View;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Assertions;

namespace Code.Runtime.UI.Services
{
    public class ScreenService :  IDisposable
    {
        private readonly Dictionary<ScreenID, (Type model, Type controller)> _screenMap = new();
        private readonly List<ActiveWindow> _activeWindows = new();
        private readonly UIFactory _uiFactory;
        private readonly ScreenModelFactory _screenModelFactory;
        private readonly IScreenControllerFactory _controllerFactory;
        
        public ScreenService(UIFactory uiFactory, 
            ScreenModelFactory screenModelFactory, 
            IScreenControllerFactory controllerFactory)
        {
            _uiFactory = uiFactory;
            _screenModelFactory = screenModelFactory;
            _controllerFactory = controllerFactory;

            _screenMap.Add(ScreenID.MainMenu, (typeof(MainMenuModel), typeof(MainMenuController)));
            _screenMap.Add(ScreenID.WeaponShop, (typeof(WeaponScreenModel), typeof(WeaponScreenController)));
            _screenMap.Add(ScreenID.AbilityMenu, (typeof(AbilityScreenModel), typeof(AbilityScreenController)));
            _screenMap.Add(ScreenID.HUD, (typeof(HudModel), (typeof(HudController))));
            _screenMap.Add(ScreenID.MessageWindow, (typeof(MessageWindowCompositeModel), typeof(MessageWindowController)));
            _screenMap.Add(ScreenID.ArenaSelectionScreen, (typeof(ArenaSelectionScreenModel), typeof(ArenaSelectionScreenController)));
            _screenMap.Add(ScreenID.RewardPopup, (typeof(RewardPopupModel), typeof(RewardPopupController)));
            _screenMap.Add(ScreenID.MissionSummaryWindow, (typeof(MissionSummaryWindowModel), typeof(MissionSummaryWindowController)));
        }

        public void Open(ScreenID screenId) => OpenInternal(screenId, null);

        public void Open(ScreenID screenId, IScreenModelDto dto) => OpenInternal(screenId, dto);

        public void Close(ScreenID screenId) => CloseInternal(x => x.Id == screenId);

        public void Close(IScreenController controller) => CloseInternal(x => Equals(x.Controller, controller));

        private void OpenInternal(ScreenID screenId, [CanBeNull] IScreenModelDto dto)
        {
            if (_activeWindows.Any(x => x.Id == screenId))
            {
                Debug.Log($"Screen {screenId} is already open.");
                return;
            }
            
            if (!_screenMap.TryGetValue(screenId, out var mc))
            {
                throw new ArgumentException($"Screen {screenId} is not registered.", nameof(screenId));
            }
            
            BaseWindowView windowView = _uiFactory.CreateScreenView(screenId);
            IScreenModel model = _screenModelFactory.CreateModel(mc.model, dto);
            IScreenController controller = _controllerFactory.CreateController(mc.controller);
            controller.InitController(model, windowView, this);

            ActiveWindow activeWindow = new ActiveWindow(controller, model, windowView, screenId);
            _activeWindows.Add(activeWindow);
            
            windowView.Show();
            
        }

        private void CloseInternal(Func<ActiveWindow, bool> predicate)
        {
            var screen = _activeWindows.FirstOrDefault(predicate);

            if (screen == null)
            {
                Debug.LogWarning("Attempted to close a screen that is not active.");
                return;
            }
            
            CloseActiveWindow(screen);
            _activeWindows.Remove(screen);
        }


        private void CloseActiveWindow(ActiveWindow activeWindow)
        {
            activeWindow.WindowView?.Close();

            if (activeWindow.Controller is IDisposable disposableController)
            {
                disposableController.Dispose();
            }

            if (activeWindow.Model is ISavableModel savableModel)
            {
                savableModel.SaveModelData();
            }
        }
        
        
        private void Cleanup()
        {
            foreach (var activeWindow in _activeWindows)
            {
                if (activeWindow.Controller is IDisposable controller)
                {
                    controller.Dispose();
                }
                if(activeWindow.WindowView)
                    activeWindow.WindowView.Close();
            }
            
            _activeWindows.Clear();
        }

        public void Dispose()
        {
            Cleanup();
        }
    }
}
