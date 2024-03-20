using Sirenix.OdinInspector.Editor;
using UnityEngine;

namespace Code.Runtime.UI.Model
{
    public class MessageWindowModel : IScreenModel
    {
        public readonly string Message;

        public MessageWindowModel(IScreenModelDto modelDto)
        {
            if (modelDto is MessageWindowDto messageDto)
            {
                Message = messageDto.Message;
            }
        }

        public void Initialize()
        {
            
        }
    }
}
