using System;
using System.Collections.Generic;
using Code.Runtime.UI.Controller;
using Code.Runtime.UI.Model;
using Code.Runtime.UI.Model.AbilityMenu;
using Code.Runtime.UI.View;
using UnityEngine;

namespace Code.Runtime.UI.Services
{
    public class ScreenService : IScreenService, IDisposable
    {
        private readonly Dictionary<ScreenID, (Type model, Type controller)> _screenMap = new();
        private readonly Dictionary<ScreenID, (BaseView view, IScreenController controller)> _activeViews = new();
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
        }

        public void Open(ScreenID screenId)
        {
            if (_screenMap.TryGetValue(screenId, out var mc))
            {
                BaseView view = _uiFactory.CreateScreenView(screenId);
                IScreenModel model = _screenModelFactory.CreateModel(mc.model);
                IScreenController controller = _controllerFactory.CreateController(mc.controller);
                controller.InitController(model, view, this);
                
                view.Show();
                
                _activeViews.Add(screenId, (view, controller));
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
                BaseView view = _uiFactory.CreateScreenView(screenId);
                IScreenModel model = _screenModelFactory.CreateModel(mc.model, dto);
                IScreenController controller = _controllerFactory.CreateController(mc.controller);
                controller.InitController(model, view, this);
                
                view.Show();
                
                _activeViews.Add(screenId, (view, controller));
            }
            else
            {
                throw new ArgumentException($"{screenId} doesnt present in the dictionary");
            }
        }

    

        public void Close(ScreenID screenID)
        {
            if (_activeViews.TryGetValue(screenID, out var activeView))
            {
                activeView.view.Close();
                
                if (activeView.controller is IDisposable controller)
                {
                    controller.Dispose();
                }

                _activeViews.Remove(screenID);
            }
        }

        public void Cleanup()
        {
            foreach (var activeView in _activeViews.Values)
            {
                if (activeView.controller is IDisposable controller)
                {
                    controller.Dispose();
                }
                if(activeView.view)
                    activeView.view.Close();
            }
            
            _activeViews.Clear();
        }

        public void Dispose()
        {
            Cleanup();
        }
    }
}
