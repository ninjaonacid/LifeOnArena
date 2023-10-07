using System;
using UnityEngine;

namespace Code.Logic.EntitiesComponents
{
    public interface IAttack
    {
        void BaseAttack();

        void SkillAttack(Vector3 castPoint);
    }
}
