using System.Threading.Tasks;
using Code.Runtime.Services;
using Code.Runtime.UI.View;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Code.Runtime.UI.Services
{
    public interface IUIFactory : IService
    {
        Task<Sprite> CreateSprite(AssetReferenceSprite spriteReference);
        BaseView CreateScreenView(ScreenID screenId);
        void CreateCore();
    }
}
