using UnityEngine;

namespace Code.Runtime.UI.View.HUD
{
    public class LookAtCamera : MonoBehaviour
    {
        private Camera _mainCamera;

        [SerializeField] private bool _flipAxis;
        private void Start()
        {
            _mainCamera = Camera.main;
        }

        private void Update()
        {
            if (_flipAxis)
            {
                var rotation = _mainCamera.transform.rotation;
                transform.LookAt(transform.position + rotation
                    * -Vector3.back,
                    rotation * Vector3.up);
            }
            else
            {
                var rotation = _mainCamera.transform.rotation;
                transform.LookAt(transform.position + rotation
                    * Vector3.back,
                    rotation * Vector3.up); 
            } 
            
        }
    }
}