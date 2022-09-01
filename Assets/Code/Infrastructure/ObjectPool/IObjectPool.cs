using Code.Services;
using Code.StaticData;
using UnityEngine;

namespace Code.Infrastructure.ObjectPool
{
    public interface IObjectPool :  IService
    {
        public GameObject GetObject(MonsterTypeId monsterTypeId, Transform parent);
        public void ReturnObject(MonsterTypeId monsterTypeId, GameObject obj);

        
    }
}