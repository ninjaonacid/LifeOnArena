using System;
using Code.CustomEvents;
using Code.Infrastructure.EventProvider;
using UnityEngine;
using VContainer;

namespace Code.Logic
{
    public class TestEventLogic : MonoBehaviour
    {
        public string Message { get; set; }
        IEventService _eventService;

        [Inject]
        public void Construct(IEventService eventService)
        {
            _eventService = eventService;
            
        }

        private void DoSomething(DoorOpenEvent eventData)
        {
            Debug.Log("KAKOGOHUYASUKA");
            _eventService.Unsubscribe<DoorOpenEvent>(DoSomething);

        }

        private void OnEnable()
        {
            _eventService.Subscribe<DoorOpenEvent>(DoSomething);
        }

        private void OnDisable()
        {
            
        }
    }


    public class DoorOpenEvent : IEvent
    {
        public float DoorSpeed;
        public DoorOpenEvent(float doorSpeed)
        {
            DoorSpeed = doorSpeed;
        }
    }
}
