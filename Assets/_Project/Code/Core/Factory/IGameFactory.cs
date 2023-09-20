using Code.Services;
using UnityEngine;

namespace Code.Core.Factory
{
    public interface IGameFactory : IService
    {
        GameObject CreateHud();
        GameObject InstantiateRegistered(string prefabPath);

        GameObject CreateLevelDoor(Vector3 position, Quaternion rotation);
    }
}