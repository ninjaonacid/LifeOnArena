using Code.Services;
using UnityEngine;

namespace Code.Infrastructure.Factory
{
    public interface IGameFactory : IService
    {
        GameObject CreateLevelEventHandler();
        GameObject CreateHud();
        GameObject InstantiateRegistered(string prefabPath);

        GameObject CreateLevelDoor(Vector3 position);
    }
}