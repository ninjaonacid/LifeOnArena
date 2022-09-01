using UnityEngine;

namespace Code.Services.Input
{
    public interface IInputService : IService
    {
        Vector2 Axis { get; }

        bool isAttackButtonUp();
        bool InventoryButton();
    }
}