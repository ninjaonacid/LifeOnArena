namespace Code.Runtime.Entity.Enemy
{
    public class EnemyWeapon : EntityWeapon
    {
        protected override void Start()
        {
            if (_weaponData != null)
            {
                EquipWeapon(_weaponData);
            }
        }
    }
}
