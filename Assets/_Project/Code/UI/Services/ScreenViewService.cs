using Code.UI.Controller;
using Code.UI.Model;
using Code.UI.View;
using VContainer;

namespace Code.UI.Services
{
    public class ScreenViewService : IScreenViewService
    {
        private readonly IUIFactory _uiFactory;
        private readonly IScreenModelFactory _screenModelFactory;
        private IObjectResolver _container;
        private ScreenBase _activeScreen;
        public ScreenViewService(IUIFactory uiFactory, IScreenModelFactory screenModelFactory, IObjectResolver container)
        {
            _uiFactory = uiFactory;
            _screenModelFactory = screenModelFactory;
            _container = container;
        }
        
        public void Show<TModel, TView, TController>(ScreenID screenID) 
            where TController : IScreenController<TModel, TView>
            where TModel : IScreenModel
            where TView : BaseView
        {
            var view = _uiFactory.CreateScreenView(screenID);
            var model = _screenModelFactory.CreateModel<TModel>(screenID);
            var controller = _container.Resolve<TController>();
            controller.InitController((TModel)model, (TView)view);
        }
        
        public void Open(ScreenID windowId)
        {

            switch (windowId)
            {
                // case ScreenID.SelectionMenu:
                //     _activeScreen?.CloseButton.onClick.Invoke();
                //     _activeScreen = _uiFactory.CreateMainMenu(this);
                //     break;
                //
                // case ScreenID.Weapon:
                //     _uiFactory.CreateWeaponWindow();
                //     break;
                //
                // case ScreenID.UpgradeMenu:
                //     _uiFactory.CreateUpgradeMenu();
                //     break;
                //
                // case ScreenID.Skills:
                //     _uiFactory.CreateSkillsMenu();
                //     break;
            }
        }
    }
}
