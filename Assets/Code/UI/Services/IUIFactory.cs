using Code.Services;
using UnityEngine;

namespace Code.UI.Services
{
    public interface IUIFactory : IService
    {
        public void CreateCore();
        WindowBase CreateSelectionMenu(IWindowService windowService);
        void CreateWeaponWindow();
    }
}
