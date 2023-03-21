using System;
using Code.CustomEvents;
using Code.Infrastructure.EventProvider;
using Code.Services;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using VContainer;

namespace Code.Logic
{
    public class NextLevelDoor : MonoBehaviour
    {
        private IEventSystem _eventSystem;
        [SerializeField] public GameObject RightDoor;
        [SerializeField] private GameObject LeftDoor;

        [Inject]
        public void Construct(IEventSystem eventSystem)
        {
            _eventSystem = eventSystem;
            _eventSystem.Subscribe<OpenDoorEvent>(OpenDoor);
    
        }

        private async void OpenDoor(OpenDoorEvent eventData)
        {
            Debug.Log(eventData.Message);
            await UniTask.Delay(1000);
            RightDoor.transform.DOLocalRotate(new Vector3(RightDoor.transform.rotation.x, 60, RightDoor.transform.rotation.z), 2f);
            LeftDoor.transform.DOLocalRotate(new Vector3(LeftDoor.transform.rotation.x, -60, LeftDoor.transform.rotation.z), 2f);
        }

        private void OnDisable()
        {
            _eventSystem.Unsubscribe<OpenDoorEvent>(OpenDoor);
        }
    }
}
