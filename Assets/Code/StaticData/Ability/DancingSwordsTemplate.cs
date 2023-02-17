using Code.Logic.Abilities;
using UnityEngine;

namespace Code.StaticData.Ability
{
    [CreateAssetMenu(menuName = "AbilityData/Cast/DancingSwords" , fileName = "DancingSwords")]
    public class DancingSwordsTemplate : AbilityTemplate<DancingSwords>
    {
        public float Damage;
        public SpinningProjectile SwordPrefab;
        public override IAbility GetAbility()
        {
            return new DancingSwords(SwordPrefab);
        }

    }
}
