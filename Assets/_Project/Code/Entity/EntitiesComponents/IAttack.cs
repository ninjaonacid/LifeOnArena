using UnityEngine;

namespace Code.Entity.EntitiesComponents
{
    public interface IAttack
    {
        void BaseAttack();

        void AoeAttack(Vector3 castPoint);
        void InvokeHit(int hitCount);
    }
}
