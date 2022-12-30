using Code.Services;
using UnityEngine;

namespace Code.Infrastructure.AssetManagment
{
    public interface IAssetsProvider : IService
    {
        GameObject Instantiate(string path);
        GameObject Instantiate(string path, Vector3 point);
        GameObject Instantiate(string path, Transform parent);
  
    }
}