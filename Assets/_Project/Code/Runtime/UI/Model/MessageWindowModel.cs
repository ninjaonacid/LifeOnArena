using Sirenix.OdinInspector.Editor;
using UnityEngine;

namespace Code.Runtime.UI.Model
{
    public class MessageWindowModel : IScreenModel
    {
        public string Message;

        public MessageWindowModel(string message)
        {
            Message = message;
        }

        public void Initialize()
        {
            
        }
    }
}
