using UnityEngine;

namespace Code.CameraLogic
{
    public class CameraFollow : MonoBehaviour
    {
        [SerializeField] private Transform _target;

        public Vector3 CamPosition;
        public float Distance;
        public float OffsetY;

        public float RotationAngleX;
        public float RotationAngleY;

        private void LateUpdate()
        {
            if (_target == null) return;
            var rotation = Quaternion.Euler(RotationAngleX, RotationAngleY, 0);

            var position = rotation * new Vector3(0, 0, -Distance) +
                           TargetFollowPosition();


            transform.rotation = rotation;
            transform.position = position;
        }

        public void Follow(GameObject followingTarget)
        {
            _target = followingTarget.transform;
        }

        private Vector3 TargetFollowPosition()
        {
            var targetPosition = _target.position;
            targetPosition.y += OffsetY;
            return targetPosition;
        }
    }
}