using System.Threading.Tasks;
using Code.Services;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Code.UI.Services
{
    public interface IUIFactory : IService
    {
        public void CreateCore();
  
        Task<Sprite> CreateSprite(AssetReferenceSprite spriteReference);
        void InitAssets();
    }
}
