using UnityEngine;

namespace Code.Runtime.Entity.Hero
{
    [RequireComponent(typeof(HeroAnimator), typeof(CharacterController))]
    public class HeroAttackComponent : EntityAttackComponent
    {
        [SerializeField] private HeroWeapon _heroWeapon;
    }
}