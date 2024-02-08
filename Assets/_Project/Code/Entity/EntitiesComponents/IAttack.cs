using UnityEngine;

namespace Code.Entity.EntitiesComponents
{
    public interface IAttack
    {
        void BaseAttack();

        void SkillAttack(Vector3 castPoint);
    }
}
