using Code.UI.Model;
using Code.UI.View;

namespace Code.UI.Services
{
    public class ScreenViewService : IScreenViewService
    {
        private readonly IUIFactory _uiFactory;
        private readonly IScreenModelFactory _screenModelFactory;
        private ScreenBase _activeScreen;
        public ScreenViewService(IUIFactory uiFactory, IScreenModelFactory screenModelFactory)
        {
            _uiFactory = uiFactory;
            _screenModelFactory = screenModelFactory;
        }
        
        public void Show(ScreenID screenID)
        {
            var view = _uiFactory.CreateScreenView(screenID);
            //var model = _screenModelFactory.CreateModel(screenID);
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
