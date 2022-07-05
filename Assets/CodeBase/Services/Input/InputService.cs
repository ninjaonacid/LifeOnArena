using UnityEngine;

namespace CodeBase.Services.Input
{
    public abstract class InputService : IInputService
    {
        protected const string Horizontal = "Horizontal";
        protected const string Vertical = "Vertical";
        private const string Attack = "Fire1";
        private const KeyCode InventoryKey = KeyCode.I;
        public abstract Vector2 Axis { get; }

        public bool InventoryButton() =>
            SimpleInput.GetKeyUp(InventoryKey);

        public bool isAttackButtonUp()
        {
            return SimpleInput.GetButtonUp(Attack);
        }

        protected static Vector2 SimpleInputAxis()
        {
            return new Vector2(SimpleInput.GetAxis(Horizontal),
                SimpleInput.GetAxis(Vertical));
        }
    }
}