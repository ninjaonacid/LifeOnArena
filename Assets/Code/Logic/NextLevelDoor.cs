using Code.Services;
using DG.Tweening;
using UnityEngine;

namespace Code.Logic
{
    public class NextLevelDoor : MonoBehaviour
    {
        private IGameEventHandler _gameEventHandler;
        [SerializeField] public GameObject RightDoor;
        [SerializeField] private GameObject LeftDoor;
        public void Construct(IGameEventHandler gameEventHandler)
        {
            _gameEventHandler  = gameEventHandler;
            _gameEventHandler.MonsterSpawnersCleared += OpenNextLevelDoor;
        }
        private void OnDestroy()
        {
            _gameEventHandler.MonsterSpawnersCleared -= OpenNextLevelDoor;
        }

        private void OpenNextLevelDoor()
        {
            RightDoor.transform.DOLocalRotate(new Vector3(RightDoor.transform.rotation.x, 60, RightDoor.transform.rotation.z), 2f);
            LeftDoor.transform.DOLocalRotate(new Vector3(LeftDoor.transform.rotation.x, -60, LeftDoor.transform.rotation.z), 2f);
        }
    }
}
