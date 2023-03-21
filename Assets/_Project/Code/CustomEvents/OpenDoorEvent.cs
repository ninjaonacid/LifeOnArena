using System.Data.SqlTypes;
using UnityEditor.VersionControl;
using UnityEngine;

namespace Code.CustomEvents
{
    public class OpenDoorEvent : IEvent
    {
        public string Message;
        public OpenDoorEvent(string message)
        {
            Message = message;
        }
    }
}
