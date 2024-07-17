using Code.Runtime.UI.Model.DTO;

namespace Code.Runtime.UI.Model.MissionSummaryWindowModel
{
    public class MissionSummaryWindowModel : IScreenModel
    {
        public IScreenModelDto MissionSummaryModelDto;

        public MissionSummaryWindowModel(IScreenModelDto missionSummaryModelDto)
        {
            MissionSummaryModelDto = missionSummaryModelDto;
        }

        public void Initialize()
        {
           
        }
    }
}
