using System.Threading.Tasks;
using Code.Services;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Code.UI.Services
{
    public interface IUIFactory : IService
    {
        public void CreateCore();
        ScreenBase CreateSelectionMenu(IWindowService windowService);
        void CreateWeaponWindow();
        void CreateUpgradeMenu();
        Task<Sprite> CreateSprite(AssetReferenceSprite spriteReference);
        void InitAssets();
        void CreateSkillsMenu();
    }
}
