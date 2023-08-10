namespace Code.UI.Services
{
    public class WindowsService : IWindowService
    {
        private readonly IUIFactory _uiFactory;
        private ScreenBase _activeScreen;
        public WindowsService(IUIFactory uiFactory)
        {
            _uiFactory = uiFactory;
        }

        public void Open(UIWindowID windowId)
        {
            switch (windowId)
            {
                case UIWindowID.SelectionMenu:
                    _activeScreen?.CloseButton.onClick.Invoke();
                    _activeScreen = _uiFactory.CreateMainMenu(this);
                    break;

                case UIWindowID.Weapon:
                    _uiFactory.CreateWeaponWindow();
                    break;

                case UIWindowID.UpgradeMenu:
                    _uiFactory.CreateUpgradeMenu();
                    break;

                case UIWindowID.Skills:
                    _uiFactory.CreateSkillsMenu();
                    break;
            }
        }
    }
}
