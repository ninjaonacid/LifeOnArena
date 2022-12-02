using Code.Services;
using DG.Tweening;
using UnityEngine;

namespace Code.Logic
{
    public class NextLevelDoor : MonoBehaviour
    {
        private IGameEventHandler _gameEventHandler;
        [SerializeField] private GameObject RightDoor;
        [SerializeField] private GameObject LeftDoor;
        private void Awake()
        {
            _gameEventHandler = AllServices.Container.Single<IGameEventHandler>();

            _gameEventHandler.MonsterSpawnersCleared += OpenNextLevelDoor;
        }

        private void OnDestroy()
        {
            _gameEventHandler.MonsterSpawnersCleared -= OpenNextLevelDoor;
        }

        private void OpenNextLevelDoor()
        {
            RightDoor.transform.DORotate(new Vector3(transform.rotation.x, 60, transform.rotation.z), 2f);
            LeftDoor.transform.DORotate(new Vector3(transform.rotation.x, -60, transform.rotation.z), 2f);
        }
    }
}
