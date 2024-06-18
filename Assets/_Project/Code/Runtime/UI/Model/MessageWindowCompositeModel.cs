using Code.Runtime.UI.Model.DTO;

namespace Code.Runtime.UI.Model
{
    public class MessageWindowCompositeModel : IScreenModel
    {
        public readonly IScreenModelDto ModelDto;
        
        public MessageWindowCompositeModel(IScreenModelDto modelDto)
        {
            ModelDto = modelDto;
        }

        public void Initialize()
        {
            
        }
    }
}
