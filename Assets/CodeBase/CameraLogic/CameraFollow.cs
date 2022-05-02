using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.CameraLogic
{
    public class CameraFollow : MonoBehaviour
    {
        [SerializeField]
        private Transform _target;
        public Vector3 CamPosition;

        public float RotationAngleX;
        public float Distance;
        public float OffsetY;

        void LateUpdate()
        {
            if (_target == null)
            {
                return;
            }
            var rotation = Quaternion.Euler(RotationAngleX, 0, 0);

            Vector3 position = rotation * new Vector3(0, 0, -Distance) + 
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
            Vector3 targetPosition = _target.position;
            targetPosition.y += OffsetY;
            return targetPosition;
        }
    }
}