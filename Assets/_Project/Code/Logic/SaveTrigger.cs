using Code.Services;
using Code.Services.SaveLoad;
using UnityEngine;
using VContainer;

namespace Code.Logic
{
    public class SaveTrigger : MonoBehaviour
    {
        private ISaveLoadService _saveLoadService;

        [SerializeField] private BoxCollider _collider;

        [Inject]
        private void Construct(ISaveLoadService saveLoadService)
        {
            _saveLoadService = saveLoadService;
        }
        private void OnTriggerEnter(Collider other)
        {
            _saveLoadService.SaveProgress();
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