using Code.Runtime.UI.Model.DTO;

namespace Code.Runtime.UI.Model
{
    public class WeaponRewardPopupModel : IScreenModel
    {
        public IScreenModelDto _weaponRewardDto;

        public WeaponRewardPopupModel(IScreenModelDto weaponRewardDto)
        {
            _weaponRewardDto = weaponRewardDto;
        }
        public void Initialize()
        {
            
        }
    }
}