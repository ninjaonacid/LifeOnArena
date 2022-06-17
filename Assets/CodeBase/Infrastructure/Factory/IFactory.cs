using System.Collections.Generic;
using CodeBase.Services;
using CodeBase.Services.PersistentProgress;
using UnityEngine;

namespace CodeBase.Infrastructure.Factory
{
    public interface IFactory : IService
    {
        GameObject InstantiateRegistered(string prefabPath, Vector3 position);
       
    }
}
