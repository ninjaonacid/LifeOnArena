using System;
using System.Collections.Generic;
using Code.UI.Controller;
using Code.UI.Model;
using Code.UI.View;

namespace Code.UI.Services
{
    public class ScreenViewService : IScreenViewService
    {
        private readonly Dictionary<ScreenID, (Type, Type)> _screenMap = new();
        private readonly IUIFactory _uiFactory;
        private readonly IScreenModelFactory _screenModelFactory;
        private readonly IScreenControllerFactory _controllerFactory;
        private ScreenBase _activeScreen;
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
                IScreenModel model = _screenModelFactory.CreateModel(mc.Item1);
                IScreenController controller = _controllerFactory.CreateController(mc.Item2);
                controller.InitController(model, view);
                
            }  
        }
    }
}
