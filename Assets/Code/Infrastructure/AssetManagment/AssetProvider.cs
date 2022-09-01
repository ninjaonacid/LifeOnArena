using UnityEngine;

namespace Code.Infrastructure.AssetManagment
{
    public class AssetProvider : IAssets
    {
        public GameObject Instantiate(string path)
        {
            var prefab = Resources.Load<GameObject>(path);
            return Object.Instantiate(prefab);
        }

        public GameObject Instantiate(string path, Vector3 point)
        {
            var prefab = Resources.Load<GameObject>(path);
            return Object.Instantiate(prefab, point, Quaternion.identity);
        }

        public GameObject Instantiate(string path, Transform parent)
        {
            var prefab = Resources.Load<GameObject>(path);
            return Object.Instantiate(prefab, parent);
        }
    }
}