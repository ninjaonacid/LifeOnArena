using UnityEngine;

namespace Code.StaticData
{

    [CreateAssetMenu(fileName = "UniqueIdSO", menuName = "Custom/UniqueIdSO")]
    public class UniqueIdSO : ScriptableObject
    {
        [SerializeField] private string id;

        private int idAsInt;

        private void OnEnable()
        {
            // Конвертируем имя в int при загрузке ScriptableObject
            idAsInt = GenerateIntId(id);
        }

        public int GetId()
        {
            return idAsInt;
        }

        private int GenerateIntId(string name)
        {
            // Генерируем int ID из имени

            int hash = 0;
            for (int i = 0; i < name.Length; i++)
            {
                hash = (hash << 5) - hash + name[i];
            }
            return hash;
        }
    }
}
