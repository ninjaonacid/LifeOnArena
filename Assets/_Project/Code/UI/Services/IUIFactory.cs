using System.Threading.Tasks;
using Code.Services;
using Code.UI.View;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Code.UI.Services
{
    public interface IUIFactory : IService
    {
        Task<Sprite> CreateSprite(AssetReferenceSprite spriteReference);
        BaseView CreateScreenView(ScreenID screenId);
        void CreateCore();
    }
}
