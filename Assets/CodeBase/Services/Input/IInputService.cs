using UnityEngine;

namespace CodeBase.Services.Input
{
    public interface IInputService : IService
    {
        Vector2 Axis { get; }

        bool isAttackButtonUp();
    }
}