using Code.Logic;
using Code.Services.Input;
using UnityEngine;
using VContainer;

namespace Code.Hero
{
    public class HeroRotation : MonoBehaviour
    {
        private IInputService _input;
        public float rotationSmooth;
        public float rotationSpeed;

        [Inject]
        private void Constructor(IInputService input)
        {
            _input = input;
        }

        private void Update()
        {
            LookRotation();
        }

        public void LookRotation()
        {
            if (_input.Axis.sqrMagnitude > Constants.Epsilon)
            {
                var rotateFrom = transform.rotation;
                var directionVector = Camera.main.transform.TransformDirection(_input.Axis);
                directionVector.y = 0;
                directionVector.Normalize();
                var rotateTo = Quaternion.LookRotation(directionVector);
                transform.rotation = Quaternion.Slerp(rotateFrom, rotateTo,  rotationSmooth * rotationSpeed * Time.deltaTime );
            }
        }
    }
}
