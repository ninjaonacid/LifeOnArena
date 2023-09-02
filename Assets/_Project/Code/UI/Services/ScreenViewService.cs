using System;
using System.Collections.Generic;
using Code.UI.Controller;
using Code.UI.Model;
using Code.UI.View;
using VContainer;

namespace Code.UI.Services
{
    public class ScreenViewService : IScreenViewService
    {
        private Dictionary<ScreenID, Type[]> _screenMap = new();
        private readonly IUIFactory _uiFactory;
        private readonly IScreenModelFactory _screenModelFactory;
        private readonly IScreenControllerFactory _controllerFactory;
        private IObjectResolver _container;
        private ScreenBase _activeScreen;
        public ScreenViewService(IUIFactory uiFactory, IScreenModelFactory screenModelFactory, 
            IScreenControllerFactory controllerFactory,
            IObjectResolver container)
        {
            _uiFactory = uiFactory;
            _screenModelFactory = screenModelFactory;
            _controllerFactory = controllerFactory;
            _container = container;

            _screenMap.Add(ScreenID.MainMenu,
                new Type[] { typeof(MainMenuModel), typeof(MainMenuView), typeof(MainMenuController) });
        }
        
        public void Show<TModel, TView, TController>(ScreenID screenID) 
            where TController : IScreenController<TModel, TView>
            where TModel : IScreenModel
            where TView : BaseView
        {
            var view = _uiFactory.CreateScreenView(screenID);
            var model = _screenModelFactory.CreateModel<TModel>();
            var controller = _controllerFactory.CreateController<TModel, TView, TController>();
            controller.InitController((TModel)model, (TView)view);
        }
        
        public void Open(ScreenID screenId)
        {
            if (_screenMap.TryGetValue(screenId, out var mvc))
            {
                var model = mvc[0];
                var view = mvc[1];
                var controller = mvc[2];
                
                
            }  
        }
    }
}
