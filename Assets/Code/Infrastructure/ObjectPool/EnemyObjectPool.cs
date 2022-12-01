using System.Collections.Generic;
using Code.Infrastructure.Factory;
using Code.StaticData;
using UnityEngine;

namespace Code.Infrastructure.ObjectPool
{
    public class EnemyObjectPool : IEnemyObjectPool
    {

        public Dictionary<MonsterTypeId, List<GameObject>> _enemyObjectsStock;

        private readonly IEnemyFactory _enemyFactory;
        public EnemyObjectPool(IEnemyFactory enemyFactory)
        {
            _enemyObjectsStock = new Dictionary<MonsterTypeId, List<GameObject>>();
            _enemyFactory = enemyFactory;
        }

        public void Cleanup()
        {
            _enemyObjectsStock.Clear();
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
