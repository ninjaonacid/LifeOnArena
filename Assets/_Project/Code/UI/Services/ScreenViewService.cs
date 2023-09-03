using System;
using System.Collections.Generic;
using Code.UI.Controller;
using Code.UI.Model;
using Code.UI.View;
using UnityEngine;

namespace Code.UI.Services
{
    public class ScreenViewService : IScreenViewService
    {
        private readonly Dictionary<ScreenID, (Type model, Type controller)> _screenMap = new();
        private readonly Dictionary<ScreenID, (BaseView view, IScreenController controller)> _activeViews = new();
        private readonly IUIFactory _uiFactory;
        private readonly IScreenModelFactory _screenModelFactory;
        private readonly IScreenControllerFactory _controllerFactory;
        public ScreenViewService(IUIFactory uiFactory, 
            IScreenModelFactory screenModelFactory, 
            IScreenControllerFactory controllerFactory)
        {
            _uiFactory = uiFactory;
            _screenModelFactory = screenModelFactory;
            _controllerFactory = controllerFactory;

            _screenMap.Add(ScreenID.MainMenu, (typeof(MainMenuModel), typeof(MainMenuController)));
        }

        public void Open(ScreenID screenId)
        {
        
            if (_screenMap.TryGetValue(screenId, out var mc))
            {
                BaseView view = _uiFactory.CreateScreenView(screenId);
                IScreenModel model = _screenModelFactory.CreateModel(mc.model);
                IScreenController controller = _controllerFactory.CreateController(mc.controller);
                controller.InitController(model, view);
                
                view.Show();
                
                _activeViews.Add(screenId, (view, controller));
            }  
        }

        public void Close(ScreenID screenID)
        {
            if (_activeViews.TryGetValue(screenID, out var activeView))
            {
                if (activeView.controller is IDisposable controller)
                {
                    controller.Dispose();
                }

                activeView.view.Close();

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
                
                activeView.view.Close();
            }
            
            _activeViews.Clear();
        }
    }
}
