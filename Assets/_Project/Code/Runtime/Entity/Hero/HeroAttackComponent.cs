using System.Collections.Generic;
using Code.Runtime.Core.Audio;
using Code.Runtime.Core.Factory;
using Code.Runtime.Entity.Enemy;
using Code.Runtime.Logic.Collision;
using Code.Runtime.Logic.Weapon;
using Code.Runtime.Services.BattleService;
using Cysharp.Threading.Tasks;
using UnityEngine;
using VContainer;

namespace Code.Runtime.Entity.Hero
{
    [RequireComponent(typeof(HeroAnimator), typeof(CharacterController))]
    public class HeroAttackComponent : EntityAttackComponent
    {
        [SerializeField] private HeroWeapon _heroWeapon;
    }
}