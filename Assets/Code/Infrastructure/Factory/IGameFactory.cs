using Code.Services;
using UnityEngine;

namespace Code.Infrastructure.Factory
{
    public interface IGameFactory : IService {

        GameObject CreateHud();
        GameObject InstantiateRegistered(string prefabPath);
        GameObject CreateInventoryDisplay();
        GameObject CreateInventorySlot(Transform parent);
    }
}