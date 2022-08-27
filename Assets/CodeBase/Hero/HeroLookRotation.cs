
using CodeBase.Logic;
using CodeBase.Services;
using CodeBase.Services.Input;
using UnityEngine;

namespace CodeBase.Hero
{
    public class HeroLookRotation : MonoBehaviour
    {
        private IInputService _input;
        public float rotationSmooth;
        public float rotationSpeed;
        private Quaternion LookDirection { get; set; }

        private void Awake()
        {
            _input = AllServices.Container.Single<IInputService>();
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
