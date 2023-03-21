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
        IEventSystem _eventSystem;

        [Inject]
        public void Construct(IEventSystem eventSystem)
        {
            _eventSystem = eventSystem;
            
        }

        
    }

}
