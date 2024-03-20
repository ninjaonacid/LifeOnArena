using Code.Runtime.UI.Model.DTO;

namespace Code.Runtime.UI.Model
{
    public class MessageWindowModel : IScreenModel
    {
        public IScreenModelDto ModelDto;
        
        public MessageWindowModel(IScreenModelDto modelDto)
        {
            ModelDto = modelDto;
        }

        public void Initialize()
        {
            
        }
    }
}
