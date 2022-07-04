using CodeBase.Services;
using UnityEngine;


namespace CodeBase.Infrastructure.Factory
{
    public interface IGameFactory : IService {

        GameObject CreateHud();
        GameObject InstantiateRegistered(string prefabPath);
        GameObject CreateInventoryDisplay();
        GameObject CreateInventorySlot(Transform parent);
    }
}