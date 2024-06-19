using Code.Runtime.UI.Model.DTO;

namespace Code.Runtime.UI.Model
{
    public class RewardPopupModel : IScreenModel
    {
        public readonly IScreenModelDto RewardDto;

        public RewardPopupModel(IScreenModelDto rewardDto)
        {
            RewardDto = rewardDto;
        }
        public void Initialize()
        {
            
        }
    }
}