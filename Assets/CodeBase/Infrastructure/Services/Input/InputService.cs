using System;
using UnityEngine;

namespace CodeBase.Infrastructure.Services.Input
{
    public abstract class InputService : IInputService
    {
        protected const string Horizontal = "Horizontal";
        protected const string Vertical = "Vertical";
        private const string Attack = "Attack";

        public abstract Vector2 Axis { get; }

        public bool isAttackButtonUp() => SimpleInput.GetButtonUp(Attack);

        protected static Vector2 SimpleInputAxis()
        {
            return new Vector2(SimpleInput.GetAxis(Horizontal),
                                        SimpleInput.GetAxis(Vertical));
        }

    }

}
