using System;
using UnityEngine;

namespace Code.Runtime.Logic.CameraLogic
{
    public class CameraFollow : MonoBehaviour
    {
        [SerializeField] private float _distance;
        [SerializeField] private float _offsetY;

        [SerializeField] private float _rotationAngleX;
        [SerializeField] private float _rotationAngleY;

        private Transform _target;
        private Transform _cameraTransform;


        private void Awake()
        {
            _cameraTransform = transform;
        }

        private void LateUpdate()
        {
            if (_target == null) return;
            
            var rotation = Quaternion.Euler(_rotationAngleX, _rotationAngleY, 0);

            var position = rotation * new Vector3(0, 0, -_distance) +
                           TargetFollowPosition();


            _cameraTransform.rotation = rotation;
            _cameraTransform.position = position;
        }

        public void Follow(GameObject followingTarget)
        {
            _target = followingTarget.transform;
        }

        private Vector3 TargetFollowPosition()
        {
            var targetPosition = _target.position;
            targetPosition.y += _offsetY;
            return targetPosition;
        }
    }
}