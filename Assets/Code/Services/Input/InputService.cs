using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

namespace Code.Services.Input
{
    public abstract class InputService : IInputService
    {
        protected const string Horizontal = "Horizontal";
        protected const string Vertical = "Vertical";
        private const string Attack = "Attack1";

        public abstract Vector2 Axis { get; }

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