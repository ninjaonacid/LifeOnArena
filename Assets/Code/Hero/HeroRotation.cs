using Code.Logic;
using Code.Services;
using Code.Services.Input;
using UnityEngine;

namespace Code.Hero
{
    public class HeroRotation : MonoBehaviour
    {
        private IInputService _input;
        public float rotationSmooth;
        public float rotationSpeed;

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
