using System.Collections.Generic;
using CodeBase.Infrastructure.Factory;
using CodeBase.StaticData;
using UnityEngine;

namespace CodeBase.Infrastructure.ObjectPool
{
    public class GameObjectPool : IObjectPool
    {
        
        public Dictionary<MonsterTypeId, List<GameObject>> _enemyObjectsStock;

        private readonly IEnemyFactory _enemyFactory;
        public GameObjectPool(IEnemyFactory enemyFactory)
        {
            _enemyObjectsStock = new Dictionary<MonsterTypeId, List<GameObject>>();
            _enemyFactory = enemyFactory;
        }

        public GameObject GetObject(MonsterTypeId monsterTypeId, Transform parent)
        {
            GameObject result = null;

            if (CheckForExist(monsterTypeId))
            {
                result = _enemyObjectsStock[monsterTypeId][0];
                _enemyObjectsStock[monsterTypeId].RemoveAt(0);
            }
            else
            {
                if (!_enemyObjectsStock.ContainsKey(monsterTypeId))
                    _enemyObjectsStock.Add(monsterTypeId, new List<GameObject>());

                result = _enemyFactory.CreateMonster(monsterTypeId, parent);
            }

            result.transform.position = parent.position;
            result.SetActive(true);
            return result;
        }

        public void ReturnObject(MonsterTypeId monsterTypeId, GameObject obj)
        {
            _enemyObjectsStock[monsterTypeId].Add(obj);
            
        }

        private bool CheckForExist(MonsterTypeId monsterTypeId)
        {
            return _enemyObjectsStock.ContainsKey(monsterTypeId) && _enemyObjectsStock[monsterTypeId].Count > 0;
            
        }
    }
}
