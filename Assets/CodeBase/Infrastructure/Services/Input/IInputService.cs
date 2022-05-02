using UnityEngine;
using CodeBase.Infrastructure;
using CodeBase.Infrastructure.Services;

namespace CodeBase.Infrastructure.Services.Input
{
    public interface IInputService : IService
    {
        Vector2 Axis { get; }

        bool isAttackButtonUp();

    }



}
