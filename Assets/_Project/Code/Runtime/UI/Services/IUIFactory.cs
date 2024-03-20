using System.Threading.Tasks;
using Code.Runtime.UI.View;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Code.Runtime.UI.Services
{
    public interface IUIFactory
    {
        Task<Sprite> CreateSprite(AssetReferenceSprite spriteReference);
        BaseWindowView CreateScreenView(ScreenID screenId);
        void CreateCore();
    }
}
