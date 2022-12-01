using Code.Services;
using DG.Tweening;
using UnityEngine;

namespace Code.Logic
{
    public class NextLevelDoor : MonoBehaviour
    {
        private IGameEventHandler _gameEventHandler;
        private void Awake()
        {
            _gameEventHandler = AllServices.Container.Single<IGameEventHandler>();

            _gameEventHandler.MonsterSpawnersCleared += OpenNextLevel;
        }

        private void OnDestroy()
        {
            _gameEventHandler.MonsterSpawnersCleared -= OpenNextLevel;
        }

        private void OpenNextLevel()
        {
            transform.DOMoveY(100, 5f);
        }
    }
}
