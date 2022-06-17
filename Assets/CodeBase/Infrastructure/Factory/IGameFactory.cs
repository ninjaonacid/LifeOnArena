using System.Collections.Generic;
using CodeBase.Enemy;
using CodeBase.Services;
using CodeBase.Services.PersistentProgress;
using CodeBase.StaticData;
using UnityEngine;


namespace CodeBase.Infrastructure.Factory
{
    public interface IGameFactory : IFactory
    {
        GameObject CreateHud();
        GameObject InstantiateRegistered(string prefabPath);
    }
}