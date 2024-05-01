using Code.Runtime.ConfigData.Identifiers;
using Code.Runtime.Logic.Collision;
using UnityEngine;

namespace Code.Runtime.Entity.Hero
{
    [RequireComponent(typeof(HeroAnimator), typeof(CharacterController))]
    public class HeroAttackComponent : EntityAttackComponent
    {
        [SerializeField] private HeroWeapon _heroWeapon;
        [SerializeField] private VisualEffectIdentifier _slashId;
        protected override void BaseAttack(CollisionData collision)
        {
            base.BaseAttack(collision);

            _visualEffectController.PlayVisualEffect(_slashId);
        }
    }
}