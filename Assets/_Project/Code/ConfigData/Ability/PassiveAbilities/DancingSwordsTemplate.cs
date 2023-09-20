using Code.Logic.Projectiles;
using UnityEngine;

namespace Code.ConfigData.Ability.PassiveAbilities
{
    [CreateAssetMenu(menuName = "AbilityData/Cast/DancingSwords" , fileName = "DancingSwords")]
    public class DancingSwordsTemplate : PassiveAbilityTemplate<DancingSwords>
    {
        public float Damage;
        public SpinningProjectile SwordPrefab;
   
        public override IPassiveAbility GetAbility()
        {
            return new DancingSwords(SwordPrefab);
        }

    }
}
