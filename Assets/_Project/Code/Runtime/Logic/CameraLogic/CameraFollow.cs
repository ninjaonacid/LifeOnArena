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
        private float _initialCameraHeight;
        private Transform _cameraTransform;

        private void Awake()
        {
            _cameraTransform = transform;
        }

        private void LateUpdate()
        {
            if (_target == null) return;
            
            Vector3 targetPosition = CalculateTargetPosition();
            Quaternion rotation = Quaternion.Euler(_rotationAngleX, _rotationAngleY, 0);
            Vector3 position = rotation * new Vector3(0, 0, -_distance) + targetPosition;

            position.y = Mathf.Clamp(position.y, _initialCameraHeight, 40f);
                
            _cameraTransform.rotation = rotation;
            _cameraTransform.position = position;
        }

        public void Follow(GameObject followingTarget)
        {
            _target = followingTarget.transform;
            var targetPos = _target.position;
            _initialCameraHeight = targetPos.y += _offsetY;
           
        }

        private Vector3 CalculateTargetPosition()
        {
            var targetPosition = _target.position;
            targetPosition.y += _offsetY;
            return targetPosition;
        }
    }
}