using CodeBase.Services;
using CodeBase.StaticData;
using UnityEngine;

namespace CodeBase.Infrastructure.ObjectPool
{
    public interface IObjectPool :  IService
    {
        public GameObject GetObject(MonsterTypeId monsterTypeId, Transform parent);
        public void ReturnObject(MonsterTypeId monsterTypeId, GameObject obj);

        
    }
}