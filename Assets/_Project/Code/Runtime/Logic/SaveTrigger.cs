using Code.Runtime.Services.SaveLoad;
using UnityEngine;
using VContainer;

namespace Code.Runtime.Logic
{
    public class SaveTrigger : MonoBehaviour
    {
        private SaveLoadService _saveLoadService;

        [SerializeField] private BoxCollider _collider;

        [Inject]
        private void Construct(SaveLoadService saveLoadService)
        {
            _saveLoadService = saveLoadService;
        }
        private void OnTriggerEnter(Collider other)
        {
            _saveLoadService.SaveData();
            Debug.Log("Progress saved");
            gameObject.SetActive(false);
        }

        private void OnDrawGizmos()
        {
            if (_collider == null)
                return;

            Gizmos.color = new Color32(30, 200, 30, 130);
            Gizmos.DrawCube(transform.position + _collider.center, _collider.size);
        }
    }
}