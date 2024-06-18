using Code.Runtime.UI.Model.DTO;

namespace Code.Runtime.UI.Model
{
    public class PopUpWindowModel : IScreenModel
    {
        public IScreenModelDto ModelDto;
        
        public PopUpWindowModel(IScreenModelDto modelDto)
        {
            ModelDto = modelDto;
        }

        public void Initialize()
        {
            
        }
    }
}
