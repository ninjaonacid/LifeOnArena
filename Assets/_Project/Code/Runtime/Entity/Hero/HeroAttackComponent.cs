using Code.Runtime.Entity.EntitiesComponents;
using UnityEngine;

namespace Code.Runtime.Entity.Hero
{
    [RequireComponent(typeof(CharacterAnimator), typeof(CharacterController))]
    public class HeroAttackComponent : EntityAttackComponent
    {
        [SerializeField] private HeroWeapon _heroWeapon;
    }
}