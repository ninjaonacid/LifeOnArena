using System.Data.SqlTypes;
using UnityEditor.VersionControl;
using UnityEngine;

namespace Code.CustomEvents
{
    public class MyZalupaEvent
    {
        public string Message;
        public MyZalupaEvent(string message)
        {
            Message = message;
        }
    }
}
