using System;
using System.Collections.Generic;
using System.Linq;
using Code.Runtime.UI.Controller;
using Code.Runtime.UI.Model;
using Code.Runtime.UI.Model.AbilityMenu;
using Code.Runtime.UI.View;
using UnityEngine;
using UnityEngine.Assertions;

namespace Code.Runtime.UI.Services
{
    public class ScreenService :  IDisposable
    {
        private readonly Dictionary<ScreenID, (Type model, Type controller)> _screenMap = new();
        private readonly List<ActiveWindow> _activeWindows = new();
        private readonly IUIFactory _uiFactory;
        private readonly ScreenModelFactory _screenModelFactory;
        private readonly IScreenControllerFactory _controllerFactory;
        
        public ScreenService(IUIFactory uiFactory, 
            ScreenModelFactory screenModelFactory, 
            IScreenControllerFactory controllerFactory)
        {
            _uiFactory = uiFactory;
            _screenModelFactory = screenModelFactory;
            _controllerFactory = controllerFactory;

            _screenMap.Add(ScreenID.MainMenu, (typeof(MainMenuModel), typeof(MainMenuController)));
            _screenMap.Add(ScreenID.Shop, (typeof(WeaponShopMenuModel), typeof(ShopMenuController)));
            _screenMap.Add(ScreenID.AbilityMenu, (typeof(AbilityMenuModel), typeof(AbilityMenuController)));
            _screenMap.Add(ScreenID.HUD, (typeof(HudModel), (typeof(HudController))));
            _screenMap.Add(ScreenID.MessageWindow, (typeof(MessageWindowModel), typeof(MessageWindowController)));
            _screenMap.Add(ScreenID.MessageWindowTimer, (typeof(MessageWindowModelWithTimer), typeof(MessageWindowWithTimerController)));
        }

        public void Open(ScreenID screenId)
        {
            if (_screenMap.TryGetValue(screenId, out var mc))
            {
                BaseWindowView windowView = _uiFactory.CreateScreenView(screenId);
                IScreenModel model = _screenModelFactory.CreateModel(mc.model);
                IScreenController controller = _controllerFactory.CreateController(mc.controller);
                controller.InitController(model, windowView, this);
                
                ActiveWindow activeWindow = new ActiveWindow(controller, windowView, screenId);
                windowView.Show();
                
                _activeWindows.Add(activeWindow);
            }
            else
            {
                throw new ArgumentException($"{screenId} doesnt present in the dictionary");
            }
        }
        public void Open(ScreenID screenId, IScreenModelDto dto)
        {
            if (_screenMap.TryGetValue(screenId, out var mc))
            {
                BaseWindowView windowView = _uiFactory.CreateScreenView(screenId);
                IScreenModel model = _screenModelFactory.CreateModel(mc.model, dto);
                IScreenController controller = _controllerFactory.CreateController(mc.controller);
                controller.InitController(model, windowView, this);

                ActiveWindow activeWindow = new ActiveWindow(controller, windowView, screenId);
                _activeWindows.Add(activeWindow);
                
                windowView.Show();
                
            }
            else
            {
                throw new ArgumentException($"{screenId} doesnt present in the dictionary");
            }
        }
        
        
        
        public void Close(ScreenID screenID)
        {
            var screen = _activeWindows.FirstOrDefault(x => x.Id == screenID);
            
            Assert.IsNotNull(screen);

            if (screen.Controller is IDisposable controller)
            {
                controller.Dispose();
            }

            if (screen.WindowView is not null)
            {
                screen.WindowView.Close();
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
