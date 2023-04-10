namespace Code.UI.Services
{
    public class WindowsService : IWindowService
    {
        private readonly IUIFactory _uiFactory;
        private WindowBase _activeWindow;
        public WindowsService(IUIFactory uiFactory)
        {
            _uiFactory = uiFactory;
        }

        public void Open(UIWindowID windowId)
        {
            switch (windowId)
            {
                case UIWindowID.SelectionMenu:
                    _activeWindow?.CloseButton.onClick.Invoke();
                    _activeWindow = _uiFactory.CreateSelectionMenu(this);
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
