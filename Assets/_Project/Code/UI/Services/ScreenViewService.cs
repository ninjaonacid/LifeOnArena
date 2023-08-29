using Code.UI.View;

namespace Code.UI.Services
{
    public class ScreenViewService : IScreenViewService
    {
        private readonly IUIFactory _uiFactory;
        private ScreenBase _activeScreen;
        public ScreenViewService(IUIFactory uiFactory)
        {
            _uiFactory = uiFactory;
        }
        
        public void Show<TScreen>(ScreenID screenID) where TScreen : IScreenView 
        {
            
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
