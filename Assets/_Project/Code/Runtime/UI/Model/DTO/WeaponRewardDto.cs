using Code.Runtime.ConfigData.Weapon;

namespace Code.Runtime.UI.Model.DTO
{
    public class WeaponRewardDto : IScreenModelDto
    {
        public readonly WeaponData WeaponData;

        public WeaponRewardDto(WeaponData weaponData)
        {
            WeaponData = weaponData;
        }
    }
}