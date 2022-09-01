using UnityEngine;

namespace Code.UI.Services
{
    public class WindowsService : IWindowService
    {
        private readonly IUIFactory _uiFactory;

        public WindowsService(IUIFactory uiFactory)
        {
            _uiFactory = uiFactory;
        }

        public void Open(UIWindowID windowId)
        {
            switch (windowId)
            {
                case UIWindowID.SelectionMenu: 
                    _uiFactory.CreateSelectionMenu();
                    break;
            }
        }
    }
}
